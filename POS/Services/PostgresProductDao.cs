using System;
using Npgsql;
using System.Collections.Generic;
using POS.Interfaces;
using POS.Models;
using POS.Helpers;
using static System.Net.Mime.MediaTypeNames;


namespace POS.Services
{
    public class PostgresProductDao : IProductDao
    {
        public PostgresProductDao() { }

        public Tuple<int, List<Product>> GetAllProducts(int page, int rowsPerPage, string searchKeyword, string categoryType, int isPriceSort)
        {
            var products = new List<Product>();
            int totalItems = 0;

             using (var connection = new NpgsqlConnection(ConnectionHelper.BuildConnectionString()))
            {
                connection.Open();

                var sql = @"
                SELECT COUNT(*) OVER() AS TotalItems, monanid, tenmonan, loaimonan, gia, mota, imagepath, trangthai 
                FROM menu
                WHERE tenmonan ILIKE @SearchKeyword and loaimonan ILIKE @Category
                ";

                if (isPriceSort == 1)
                {
                    sql += "ORDER BY gia ASC ";
                }
                else if (isPriceSort == 2)
                {
                    sql += "ORDER BY gia DESC ";
                }

                sql += "offset @Skip rows fetch next @Take rows only";


                var skip = (page - 1) * rowsPerPage;
                var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Skip", skip);
                command.Parameters.AddWithValue("@Take", rowsPerPage);
                command.Parameters.AddWithValue("@SearchKeyword", $"%{searchKeyword}%");
                command.Parameters.AddWithValue("@Category", $"%{categoryType}%");

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (totalItems == 0)
                        {
                            totalItems = reader.GetInt32(reader.GetOrdinal("TotalItems"));
                        }

                        var product = new Product
                        {
                            ProductID = reader.GetInt32(reader.GetOrdinal("monanid")),
                            Name = reader.GetString(reader.GetOrdinal("tenmonan")),
                            Category = reader.GetString(reader.GetOrdinal("loaimonan")),
                            Price = reader.GetInt32(reader.GetOrdinal("gia")),
                            Description = reader.IsDBNull(reader.GetOrdinal("mota")) ? null : reader.GetString(reader.GetOrdinal("mota")),
                            ImagePath = reader.IsDBNull(reader.GetOrdinal("imagepath")) ? null : reader.GetString(reader.GetOrdinal("imagepath")),
                            Status = reader.GetBoolean(reader.GetOrdinal("trangthai"))
                        };
                        products.Add(product);
                    }
                }
            }

            return new Tuple<int, List<Product>>(totalItems, products);
        }

        // Thêm sản phẩm mới
        public int InsertProduct(Product product)
        {
            int newId;
             using (var connection = new NpgsqlConnection(ConnectionHelper.BuildConnectionString()))
            {
                connection.Open();

                var sql = @"
                INSERT INTO menu (tenmonan, loaimonan, gia, mota, imagepath, trangthai)
                VALUES (@Name, @Category, @Price, @Description, @ImagePath, @Status)
                RETURNING monanid";

                var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Category", product.Category);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Description", (object)product.Description ?? DBNull.Value);
                command.Parameters.AddWithValue("@ImagePath", (object)product.ImagePath ?? DBNull.Value);
                command.Parameters.AddWithValue("@Status", product.Status);

                newId = Convert.ToInt32(command.ExecuteScalar());
            }

            return newId;
        }

        // Cập nhật thông tin sản phẩm
        public bool UpdateProduct(Product product)
        {
            int rowsAffected;
             using (var connection = new NpgsqlConnection(ConnectionHelper.BuildConnectionString()))
            {
                connection.Open();

                // Check if the product exists
                var checkSql = "SELECT COUNT(*) FROM menu WHERE monanid = @ProductID";
                var checkCommand = new NpgsqlCommand(checkSql, connection);
                checkCommand.Parameters.AddWithValue("@ProductID", product.ProductID);
                var productExists = (long)checkCommand.ExecuteScalar() > 0;

                if (!productExists)
                {
                    return false; // Product does not exist, return false
                }

                var sql = @"
                UPDATE menu
                SET tenmonan = @Name, loaimonan = @Category, gia = @Price, mota = @Description, imagepath = @ImagePath, trangthai = @Status
                WHERE monanid = @ProductID";

                var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Category", product.Category);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Description", (object)product.Description ?? DBNull.Value);
                command.Parameters.AddWithValue("@ImagePath", (object)product.ImagePath ?? DBNull.Value);
                command.Parameters.AddWithValue("@Status", product.Status);
                command.Parameters.AddWithValue("@ProductID", product.ProductID);

                rowsAffected = command.ExecuteNonQuery();
            }

            return rowsAffected > 0;
        }

        // Xóa sản phẩm theo Name
        public void RemoveProductById(int productId)
        {
             using (var connection = new NpgsqlConnection(ConnectionHelper.BuildConnectionString()))
            {
                connection.Open();

                // Check if the product exists
                var checkSql = "SELECT COUNT(*) FROM menu WHERE monanid = @ProductID";
                var checkCommand = new NpgsqlCommand(checkSql, connection);
                checkCommand.Parameters.AddWithValue("@ProductID", productId);
                var productExists = (long)checkCommand.ExecuteScalar() > 0;

                if (!productExists)
                {
                    return;
                }

                var sql = "DELETE FROM menu WHERE monanid = @ProductID";
                var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ProductID", productId);

                command.ExecuteNonQuery();
            }
        }
    }
}