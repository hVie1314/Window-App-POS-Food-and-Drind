using System;
using Npgsql;
using System.Collections.Generic;
using POS.Interfaces;
using POS.Models;
using POS.Helpers;

namespace POS.Services.DAO
{
    public class PostgresInvoiceDao : IInvoiceDao
    {
        public PostgresInvoiceDao() { }

        public Tuple<int, List<Invoice>> GetAllInvoices(string hoadonID, int page, int rowsPerPage)
        {
            var invoices = new List<Invoice>();
            int totalItems = 0;

            using (var connection = new NpgsqlConnection(ConnectionHelper.BuildConnectionString()))
            {
                connection.Open();

                var sql = @"
                SELECT COUNT(*) OVER() AS TotalItems, hoadonid, ngaylaphoadon, tongtien, phuongthucthanhtoan, khachhangid, nhanvienid, giamgia, thuevat, ghichu
                FROM hoadon";
                if (!string.IsNullOrEmpty(hoadonID))
                {
                    sql += " WHERE hoadonid = @hoadonID";
                }
                sql +=@" OFFSET @Skip LIMIT @Take";
                

                var skip = (page - 1) * rowsPerPage;
                var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Skip", skip);
                command.Parameters.AddWithValue("@Take", rowsPerPage);
                if (!string.IsNullOrEmpty(hoadonID))
                {
                    command.Parameters.AddWithValue("@hoadonID", Int32.Parse(hoadonID));
                }

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (totalItems == 0)
                        {
                            totalItems = reader.GetInt32(reader.GetOrdinal("TotalItems"));
                        }

                        var invoice = new Invoice
                        {
                            InvoiceID = reader.GetInt32(reader.GetOrdinal("hoadonid")),
                            InvoiceDate = reader.GetDateTime(reader.GetOrdinal("ngaylaphoadon")),
                            TotalAmount = reader.GetDouble(reader.GetOrdinal("tongtien")),
                            PaymentMethod = reader.IsDBNull(reader.GetOrdinal("phuongthucthanhtoan")) ? null : reader.GetString(reader.GetOrdinal("phuongthucthanhtoan")),
                            CustomerID = reader.GetInt32(reader.GetOrdinal("khachhangid")),
                            EmployeeID = reader.GetInt32(reader.GetOrdinal("nhanvienid")),
                            Discount = reader.GetFloat(reader.GetOrdinal("giamgia")),
                            Tax = reader.GetDouble(reader.GetOrdinal("thuevat")),
                            Note = reader.IsDBNull(reader.GetOrdinal("ghichu")) ? null : reader.GetString(reader.GetOrdinal("ghichu"))
                        };
                        invoices.Add(invoice);
                    }
                }
            }

            return new Tuple<int, List<Invoice>>(totalItems, invoices);
        }

        public int InsertInvoice(Invoice invoice)
        {
            int newId;
            using (var connection = new NpgsqlConnection(ConnectionHelper.BuildConnectionString()))
            {
                connection.Open();

                var sql = @"
                INSERT INTO hoadon (ngaylaphoadon, tongtien, phuongthucthanhtoan, khachhangid, nhanvienid, giamgia, thuevat, ghichu)
                VALUES (@InvoiceDate, @TotalAmount, @PaymentMethod, @CustomerID, @EmployeeID, @Discount, @VAT, @Note)
                RETURNING hoadonid";

                var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
                command.Parameters.AddWithValue("@TotalAmount", invoice.TotalAmount);
                command.Parameters.AddWithValue("@PaymentMethod", invoice.PaymentMethod ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CustomerID", invoice.CustomerID);
                command.Parameters.AddWithValue("@EmployeeID", invoice.EmployeeID);
                command.Parameters.AddWithValue("@Discount", invoice.Discount);
                command.Parameters.AddWithValue("@VAT", invoice.Tax);
                command.Parameters.AddWithValue("@Note", invoice.Note ?? (object)DBNull.Value);

                newId = Convert.ToInt32(command.ExecuteScalar());
            }

            return newId;
        }

        public bool UpdateInvoice(Invoice invoice)
        {
            int rowsAffected;
            using (var connection = new NpgsqlConnection(ConnectionHelper.BuildConnectionString()))
            {
                connection.Open();

                // Check if the invoice exists
                var checkSql = "SELECT COUNT(*) FROM hoadon WHERE hoadonid = @InvoiceID";
                var checkCommand = new NpgsqlCommand(checkSql, connection);
                checkCommand.Parameters.AddWithValue("@InvoiceID", invoice.InvoiceID);
                var invoiceExists = (long)checkCommand.ExecuteScalar() > 0;

                if (!invoiceExists)
                {
                    return false; // Invoice does not exist, return false
                }

                var sql = @"
                UPDATE hoadon
                SET ngaylaphoadon = @InvoiceDate, tongtien = @TotalAmount, phuongthucthanhtoan = @PaymentMethod, khachhangid = @CustomerID, nhanvienid = @EmployeeID, giamgia = @Discount, thuevat = @VAT, ghichu = @Note
                WHERE hoadonid = @InvoiceID";

                var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
                command.Parameters.AddWithValue("@TotalAmount", invoice.TotalAmount);
                command.Parameters.AddWithValue("@PaymentMethod", invoice.PaymentMethod);
                command.Parameters.AddWithValue("@CustomerID", invoice.CustomerID);
                command.Parameters.AddWithValue("@EmployeeID", invoice.EmployeeID);
                command.Parameters.AddWithValue("@Discount", invoice.Discount);
                command.Parameters.AddWithValue("@VAT", invoice.Tax);
                command.Parameters.AddWithValue("@Note", invoice.Note);
                command.Parameters.AddWithValue("@InvoiceID", invoice.InvoiceID);

                rowsAffected = command.ExecuteNonQuery();
            }

            return rowsAffected > 0;
        }

        public void RemoveInvoiceById(int invoiceId)
        {
            using (var connection = new NpgsqlConnection(ConnectionHelper.BuildConnectionString()))
            {
                connection.Open();

                // Check if the invoice exists
                var checkSql = "SELECT COUNT(*) FROM hoadon WHERE hoadonid = @InvoiceID";
                var checkCommand = new NpgsqlCommand(checkSql, connection);
                checkCommand.Parameters.AddWithValue("@InvoiceID", invoiceId);
                var invoiceExists = (long)checkCommand.ExecuteScalar() > 0;

                if (!invoiceExists)
                {
                    return;
                }

                var sql = "DELETE FROM hoadon WHERE hoadonid = @InvoiceID";
                var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@InvoiceID", invoiceId);

                command.ExecuteNonQuery();
            }
        }
    }
}