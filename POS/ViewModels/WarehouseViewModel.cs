using System.Collections.ObjectModel;
using System.ComponentModel;
using POS.Models;
using POS.Services.DAO;
using POS.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;


namespace POS.ViewModels
{
    public sealed partial class WarehouseViewModel : INotifyPropertyChanged
    {
        private IWarehouseDao _warehouseDao = new PostgresWarehouseDao();
        public ObservableCollection<Warehouse> Warehouses { get; private set; }

        public string Keyword { get; set; } = "";
        public int CurrentPage { get; set; } = 1;
        public int RowsPerPage { get; set; } = 12;
        public int TotalPages { get; set; } = 0;
        public int TotalItems { get; set; } = 0;

        private string _currentSortColumn = null;
        private string _currentSortDirection = null;


        public WarehouseViewModel()
        {
            GetAllWarehouses();
        }

        public void GetAllWarehouses()
        {
            var (totalItems, warehouses) = _warehouseDao.GetAllWarehouses(CurrentPage, RowsPerPage, Keyword, _currentSortColumn, _currentSortDirection);
            Warehouses = new FullObservableCollection<Warehouse>(warehouses);

            TotalItems = totalItems;
            TotalPages = (TotalItems / RowsPerPage)
                + ((TotalItems % RowsPerPage == 0)
                        ? 0 : 1);
        }

        public void LoadWarehouses(int page)
        {
            CurrentPage = page;
            GetAllWarehouses();
        }

        // Add feature
        public bool AddWarehouse(Warehouse warehouse)
        {
            try
            {
                // Save to database
                int newId = _warehouseDao.InsertWarehouse(warehouse);
                warehouse.WarehouseID = newId;

                // Recall from database to get the full object
                var (totalItems, warehouses) = _warehouseDao.GetAllWarehouses(CurrentPage, RowsPerPage, Keyword, _currentSortColumn, _currentSortDirection);
                Warehouses = new FullObservableCollection<Warehouse>(warehouses);

                // Notify the change to UI
                OnPropertyChanged(nameof(Warehouses));

                return true;
            }
            catch (Exception ex)
            {
                // Log err if failed
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Warehouse selection getter and setter    
        private Warehouse _selectedWarehouse;
        public Warehouse SelectedWarehouse
        {
            get => _selectedWarehouse;
            set
            {
                if (_selectedWarehouse != value)
                {
                    if (_selectedWarehouse != null)
                    {
                        _selectedWarehouse.PropertyChanged -= OnWarehousePropertyChanged;
                    }

                    _selectedWarehouse = value;

                    if (_selectedWarehouse != null)
                    {
                        _selectedWarehouse.PropertyChanged += OnWarehousePropertyChanged;
                    }

                    OnPropertyChanged(nameof(SelectedWarehouse));
                }
            }
        }

        // Delete feature
        public bool DeleteWarehouse(Warehouse warehouse)
        {
            try
            {
                // Delete from database
                _warehouseDao.RemoveWarehouseById(warehouse.WarehouseID);

                // Delete from the displayed list
                Warehouses.Remove(warehouse);
                OnPropertyChanged(nameof(Warehouses));

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when delete: {ex.Message}");
                return false;
            }
        }

        // Update feature
        private void OnWarehousePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var warehouse = sender as Warehouse;
            if (warehouse != null)
            {
                // Update the database with the changed property
                UpdateWarehouseInDatabase(warehouse);
            }
        }

        private void UpdateWarehouseInDatabase(Warehouse warehouse)
        {
            try
            {
                _warehouseDao.UpdateWarehouse(warehouse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when updating warehouse: {ex.Message}");
            }
        }

        // Sorting feature
        public void SortWarehouses(string columnName)
        {
            if (columnName == _currentSortColumn)
            {
                // Change sort direction
                _currentSortDirection = _currentSortDirection switch
                {
                    null => "ASC",   
                    "ASC" => "DESC", 
                    "DESC" => null, 
                    _ => null
                };
            }
            else
            {
                // Change sort column
                _currentSortColumn = columnName;
                _currentSortDirection = "ASC";
            }

            // Recall the data
            LoadWarehouses(CurrentPage);
        }


        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}