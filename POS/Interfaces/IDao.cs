using POS.Models;
using System;
using System.Collections.Generic;


namespace POS.Interfaces
{
    public interface IProductDao
    {
        Tuple<int, List<Product>> GetAllProducts(
            int page = 1,

            int rowsPerPage = 5,

            string searchKeyword = "",
            string categoryType = "",
            int isPriceSort = 0 // 0: không sắp xếp, 1: sắp xếp tăng dần, 2: sắp xếp giảm dần
        );
        int InsertProduct(Product product);
        bool UpdateProduct(Product product);
        void RemoveProductById(int productId);
    }

    public interface IEmployeeDao
    {
        Tuple<int, List<Employee>> GetAllEmployees(
            int page = 1,
            int rowsPerPage = 10,
            string searchKeyword = "",
            string position = "",
            int isSalarySort = 0 // 0: không sắp xếp, 1: sắp xếp tăng dần, 2: sắp xếp giảm dần
        );
        int InsertEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
        void RemoveEmployeeById(int employeeId);
    }

    public interface ICustomerDao
    {
        Tuple<int, List<Customer>> GetAllCustomers(
            int page = 1,
            int rowsPerPage = 10,
            string searchKeyword = "",
            string customerType = ""
        );
        int InsertCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        void RemoveCustomerById(int customerId);
    }

    public interface IWarehouseDao
    {
        Tuple<int, List<Warehouse>> GetAllWarehouses(
            int page = 1, 
            int rowsPerPage = 12, 
            string searchKeyword = "", 
            string sortColumn = null, 
            string sortDirection = null
        );
        int InsertWarehouse(Warehouse warehouse);
        bool UpdateWarehouse(Warehouse warehouse);
        void RemoveWarehouseById(int warehouseId);
    }

    public interface IInvoiceDao
    {
        Tuple<int, List<Invoice>> GetAllInvoices(string searchKeyword = "",
            int page = 1,
            int rowsPerPage = 10
        );
        int InsertInvoice(Invoice invoice);
        bool UpdateInvoice(Invoice invoice);
        void RemoveInvoiceById(int invoiceId);
    }

    public interface IInvoiceDetailDao
    {
        Tuple<int, List<InvoiceDetail>> GetAllInvoiceDetails(
            int page = 1,
            int rowsPerPage = 10    
        );
        public Tuple<int, List<InvoiceDetailWithProductInfo>> GetAllInvoiceDetailsWithProductInformation(int invoiceId);
        int InsertInvoiceDetail(InvoiceDetail invoiceDetail);
        bool UpdateInvoiceDetail(InvoiceDetail invoiceDetail);
        void RemoveInvoiceDetailById(int invoiceDetailId);
    }

    public interface IWorkShiftDao
    {
        Tuple<int, List<WorkShift>> GetAllWorkShifts(
            int page = 1,
            int rowsPerPage = 10
        );
        int InsertWorkShift(WorkShift workShift);
        bool UpdateWorkShift(WorkShift workShift);
        void RemoveWorkShiftById(int workShiftId);
    }

    public interface IInventoryReportDao
    {
        Tuple<int, List<InventoryReport>> GetAllInventoryReports(
            int page = 1,
            int rowsPerPage = 10
        );
        int InsertInventoryReport(InventoryReport inventoryReport);
        bool UpdateInventoryReport(InventoryReport inventoryReport);
        void RemoveInventoryReportById(int inventoryReportId);
    }

    public interface IRevenueReportDao
    {
        Tuple<int, List<RevenueReport>> GetAllRevenueReports(
            int page = 1,
            int rowsPerPage = 10
        );
        int InsertRevenueReport(RevenueReport revenueReport);
        bool UpdateRevenueReport(RevenueReport revenueReport);
        void RemoveRevenueReportById(int revenueReportId);
    }

    public interface IDiscountDao
    {
        Tuple<int, List<Discount>> GetAllDiscount(
            int page = 1,
            int rowsPerPage = 10
        );
        int InsertDiscount(string discountCode, int discountValue);
        void RemoveDiscountByCode(string discountCode);
    }
}