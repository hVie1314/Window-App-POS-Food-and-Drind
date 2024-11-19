using System;
using Npgsql;
using System.Collections.Generic;
using POS.Interfaces;
using POS.Models;
using POS.Helpers;

namespace POS.Services
{
    public class PostgresWarehouseDao : IWarehouseDao
    {
        public PostgresWarehouseDao() { }

        public Tuple<int, List<Warehouse>> GetAllWarehouses(int page, int rowsPerPage, string searchKeyword)
        {
            var warehouses = new List<Warehouse>();
            int totalRecords = 0;

            using (var connection = new NpgsqlConnection(ConnectionHelper.BuildConnectionString()))
            {
                connection.Open();

                var countSql = "SELECT COUNT(*) FROM khohang";
                var countCommand = new NpgsqlCommand(countSql, connection);
                totalRecords = Convert.ToInt32(countCommand.ExecuteScalar());

                var sql = @"
                    SELECT khoid, tennguyenlieu, soluongton, donvitinh, ngaynhapkho, ngayhethan
                    FROM khohang
                    WHERE tennguyenlieu ILIKE @SearchKeyword 
                    ORDER BY khoid
                    LIMIT @RowsPerPage OFFSET @Offset";

                var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@SearchKeyword", $"%{searchKeyword}%");
                command.Parameters.AddWithValue("@RowsPerPage", rowsPerPage);
                command.Parameters.AddWithValue("@Offset", (page - 1) * rowsPerPage);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var warehouse = new Warehouse
                        {
                            WarehouseID = reader.GetInt32(reader.GetOrdinal("khoid")),
                            IngredientName = reader.GetString(reader.GetOrdinal("tennguyenlieu")),
                            StockQuantity = reader.GetInt32(reader.GetOrdinal("soluongton")),
                            Unit = reader.GetString(reader.GetOrdinal("donvitinh")),
                            EntryDate = reader.GetDateTime(reader.GetOrdinal("ngaynhapkho")),
                            ExpirationDate = reader.GetDateTime(reader.GetOrdinal("ngayhethan"))
                        };
                        warehouses.Add(warehouse);
                    }
                }
            }

            return new Tuple<int, List<Warehouse>>(totalRecords, warehouses);
        }

        public int InsertWarehouse(Warehouse warehouse)
        {
            int newId;
            using (var connection = new NpgsqlConnection(ConnectionHelper.BuildConnectionString()))
            {
                connection.Open();

                var sql = @"
                    INSERT INTO khohang (tennguyenlieu, soluongton, donvitinh, ngaynhapkho, ngayhethan)
                    VALUES (@IngredientName, @StockQuantity, @Unit, @EntryDate, @ExpirationDate)
                    RETURNING khoid";

                var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IngredientName", warehouse.IngredientName);
                command.Parameters.AddWithValue("@StockQuantity", warehouse.StockQuantity);
                command.Parameters.AddWithValue("@Unit", warehouse.Unit);
                command.Parameters.AddWithValue("@EntryDate", warehouse.EntryDate);
                command.Parameters.AddWithValue("@ExpirationDate", warehouse.ExpirationDate);

                newId = Convert.ToInt32(command.ExecuteScalar());
            }

            return newId;
        }

        public bool UpdateWarehouse(Warehouse warehouse)
        {
            int rowsAffected;
            using (var connection = new NpgsqlConnection(ConnectionHelper.BuildConnectionString()))
            {
                connection.Open();

                // Check if the warehouse exists
                var checkSql = "SELECT COUNT(*) FROM khohang WHERE khoid = @WarehouseID";
                var checkCommand = new NpgsqlCommand(checkSql, connection);
                checkCommand.Parameters.AddWithValue("@WarehouseID", warehouse.WarehouseID);
                var warehouseExists = (long)checkCommand.ExecuteScalar() > 0;

                if (!warehouseExists)
                {
                    return false; // Warehouse does not exist, return false
                }

                var sql = @"
                    UPDATE khohang
                    SET tennguyenlieu = @IngredientName, soluongton = @StockQuantity, donvitinh = @Unit, ngaynhapkho = @EntryDate, ngayhethan = @ExpirationDate
                    WHERE khoid = @WarehouseID";

                var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IngredientName", warehouse.IngredientName);
                command.Parameters.AddWithValue("@StockQuantity", warehouse.StockQuantity);
                command.Parameters.AddWithValue("@Unit", warehouse.Unit);
                command.Parameters.AddWithValue("@EntryDate", warehouse.EntryDate);
                command.Parameters.AddWithValue("@ExpirationDate", warehouse.ExpirationDate);
                command.Parameters.AddWithValue("@WarehouseID", warehouse.WarehouseID);

                rowsAffected = command.ExecuteNonQuery();
            }

            return rowsAffected > 0;
        }

        public void RemoveWarehouseById(int warehouseId)
        {
            using (var connection = new NpgsqlConnection(ConnectionHelper.BuildConnectionString()))
            {
                connection.Open();

                // Check if the warehouse exists
                var checkSql = "SELECT COUNT(*) FROM khohang WHERE khoid = @WarehouseID";
                var checkCommand = new NpgsqlCommand(checkSql, connection);
                checkCommand.Parameters.AddWithValue("@WarehouseID", warehouseId);
                var warehouseExists = (long)checkCommand.ExecuteScalar() > 0;

                if (!warehouseExists)
                {
                    return;
                }

                var sql = "DELETE FROM khohang WHERE khoid = @WarehouseID";
                var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@WarehouseID", warehouseId);

                command.ExecuteNonQuery();
            }
        }
    }
}