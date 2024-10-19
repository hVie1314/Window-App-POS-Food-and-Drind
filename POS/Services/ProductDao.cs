//using System.Collections.Generic;
//using POS.Interfaces;
//using POS.Models;
//using Microsoft.Data.SqlClient;
//using System;

//namespace POS.Services
//{
//    public class ProductDao : IDao<Product>
//    {
//        // cần update để lưu trữ connection string vào appsettings.json
//        // trong slide
//        private string connectionString = @"Server=HOANGVIET\SQLEXPRESS;Database=POS_database;User ID=sa;Password=123456;TrustServerCertificate=True";

//        public IEnumerable<Product> GetAll()
//        {
//            var products = new List<Product>();

//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                string query = "SELECT * FROM Product";

//                using (SqlCommand command = new SqlCommand(query, connection))
//                {
//                    using (SqlDataReader reader = command.ExecuteReader())
//                    {
//                        while (reader.Read())
//                        {
//                            var product = new Product
//                            {
//                                ProductID = reader.GetInt32(0),
//                                Name = reader.GetString(1),
//                                Price = reader.GetInt32(2),
//                                Category = reader.GetString(3),
//                                StockQuantity = reader.GetInt32(4),
//                                IsAvailable = reader.GetString(5),
//                                ImagePath = reader.IsDBNull(6) ? null : reader.GetString(6),
//                                Type = reader.GetString(7)
//                            };

//                            products.Add(product);
//                        }
//                    }
//                }
//            }

//            return products;
//        }

//        public Product GetById(int id)
//        {
//            Product product = null;

//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                string query = "SELECT * FROM Product WHERE ProductID = @id";

//                using (SqlCommand command = new SqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@id", id);

//                    using (SqlDataReader reader = command.ExecuteReader())
//                    {
//                        if (reader.Read())
//                        {
//                            product = new Product
//                            {
//                                ProductID = reader.GetInt32(0),
//                                Name = reader.GetString(1),
//                                Price = reader.GetInt32(2),
//                                Category = reader.GetString(3),
//                                StockQuantity = reader.GetInt32(4),
//                                IsAvailable = reader.GetString(5),
//                                ImagePath = reader.IsDBNull(6) ? null : reader.GetString(6),
//                                Type = reader.GetString(7)
//                            };
//                        }
//                    }
//                }
//            }

//            return product;
//        }

//        public void Add(Product product)
//        {
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                string query = "INSERT INTO Product (Name, Price, Category, StockQuantity, IsAvailable, ImagePath, Type) " +
//                    "VALUES (@name, @price, @category, @stockQuantity, @isAvailable, @imagePath, @type)";

//                using (SqlCommand command = new SqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@name", product.Name);
//                    command.Parameters.AddWithValue("@price", product.Price);
//                    command.Parameters.AddWithValue("@category", product.Category);
//                    command.Parameters.AddWithValue("@stockQuantity", product.StockQuantity);
//                    command.Parameters.AddWithValue("@isAvailable", product.IsAvailable);
//                    command.Parameters.AddWithValue("@imagePath", product.ImagePath ?? (object)DBNull.Value);
//                    command.Parameters.AddWithValue("@type", product.Type);

//                    command.ExecuteNonQuery();
//                }
//            }
//        }

//        public void Update(Product product)
//        {
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                string query = "UPDATE Product SET Name = @name, Price = @price, Category = @category, StockQuantity = @stockQuantity, IsAvailable = @isAvailable, ImagePath = @imagePath, Type = @type WHERE ProductID = @id";

//                using (SqlCommand command = new SqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@id", product.ProductID);
//                    command.Parameters.AddWithValue("@name", product.Name);
//                    command.Parameters.AddWithValue("@price", product.Price);
//                    command.Parameters.AddWithValue("@category", product.Category);
//                    command.Parameters.AddWithValue("@stockQuantity", product.StockQuantity);
//                    command.Parameters.AddWithValue("@isAvailable", product.IsAvailable);
//                    command.Parameters.AddWithValue("@imagePath", product.ImagePath ?? (object)DBNull.Value);
//                    command.Parameters.AddWithValue("@type", product.Type);

//                    command.ExecuteNonQuery();
//                }
//            }
//        }

//        public void Delete(int id)
//        {
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                string query = "DELETE FROM Product WHERE ProductID = @id";
//                using (SqlCommand command = new SqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@id", id);
//                    command.ExecuteNonQuery();
//                }
//            }
//        }
//    }
//}
