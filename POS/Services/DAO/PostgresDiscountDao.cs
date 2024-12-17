using System;
using Npgsql;
using System.Collections.Generic;
using POS.Interfaces;
using POS.Models;
using POS.Helpers;

namespace POS.Services.DAO
{
    public class PostgresDiscountDao : IDiscountDao
    {
        public PostgresDiscountDao() { }

        public Tuple<int, List<Discount>> GetAllDiscount(int page, int rowsPerPage)
        {
            var discounts = new List<Discount>();
            int totalItems = 0;

            using (var connection = new NpgsqlConnection(ConnectionHelper.BuildConnectionString()))
            {
                connection.Open();

                var sql = @"
                        SELECT COUNT(*) OVER() AS TotalItems, id, MaGiamGia, TriGia
                        FROM MaGiamGia
                        OFFSET @Skip LIMIT @Take";

                var skip = (page - 1) * rowsPerPage;
                var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Skip", skip);
                command.Parameters.AddWithValue("@Take", rowsPerPage);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (totalItems == 0)
                        {
                            totalItems = reader.GetInt32(reader.GetOrdinal("TotalItems"));
                        }

                        var discount = new Discount
                        {
                            DiscountId = reader.GetInt32(reader.GetOrdinal("id")),
                            DiscountCode = reader.GetString(reader.GetOrdinal("MaGiamGia")),
                            DiscountValue = reader.GetInt32(reader.GetOrdinal("TriGia"))
                        };
                        discounts.Add(discount);
                    }
                }
            }

            return new Tuple<int, List<Discount>>(totalItems, discounts);
        }

        public int InsertDiscount(string discountCode, int discountValue)
        {
            int newId;
            using (var connection = new NpgsqlConnection(ConnectionHelper.BuildConnectionString()))
            {
                connection.Open();

                var sql = @"
                        INSERT INTO MaGiamGia (MaGiamGia, TriGia)
                        VALUES (@DiscountCode, @DiscountValue)
                        RETURNING id";

                var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@DiscountCode", discountCode);
                command.Parameters.AddWithValue("@DiscountValue", discountValue);

                newId = Convert.ToInt32(command.ExecuteScalar());
            }

            return newId;
        }

        public void RemoveDiscountByCode(string discountCode)
        {
            using (var connection = new NpgsqlConnection(ConnectionHelper.BuildConnectionString()))
            {
                connection.Open();

                var sql = "DELETE FROM MaGiamGia WHERE MaGiamGia = @DiscountCode";
                var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@DiscountCode", discountCode);

                command.ExecuteNonQuery();
            }
        }
    }
}
