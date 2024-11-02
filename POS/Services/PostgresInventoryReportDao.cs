using System;
using Npgsql;
using System.Collections.Generic;
using POS.Interfaces;
using POS.Models;
using POS.Helpers;

namespace POS.Services
{
    public class PostgresInventoryReportDao : IInventoryReportDao
    {
        public PostgresInventoryReportDao() { }

        public Tuple<int, List<InventoryReport>> GetAllInventoryReports(int page, int rowsPerPage)
        {
            var reports = new List<InventoryReport>();
            int totalItems = 0;

            using (var connection = new NpgsqlConnection(ConnectionHelper.BuildConnectionString()))
            {
                connection.Open();

                var sql = @"
                SELECT COUNT(*) OVER() AS TotalItems, baocaotonid, ngaybaocao, nguyenlieuid, soluongconlai
                FROM baocaotonkho
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

                        var report = new InventoryReport
                        {
                            ReportID = reader.GetInt32(reader.GetOrdinal("baocaotonid")),
                            ReportDate = reader.GetDateTime(reader.GetOrdinal("ngaybaocao")),
                            IngredientID = reader.GetInt32(reader.GetOrdinal("nguyenlieuid")),
                            RemainingQuantity = reader.GetInt32(reader.GetOrdinal("soluongconlai"))
                        };
                        reports.Add(report);
                    }
                }
            }

            return new Tuple<int, List<InventoryReport>>(totalItems, reports);
        }

        // Thêm báo cáo tồn kho mới
        public int InsertInventoryReport(InventoryReport report)
        {
            int newId;
            using (var connection = new NpgsqlConnection(ConnectionHelper.BuildConnectionString()))
            {
                connection.Open();

                var sql = @"
                INSERT INTO baocaotonkho (ngaybaocao, nguyenlieuid, soluongconlai)
                VALUES (@ReportDate, @IngredientID, @RemainingQuantity)
                RETURNING baocaotonid";

                var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ReportDate", report.ReportDate);
                command.Parameters.AddWithValue("@IngredientID", report.IngredientID);
                command.Parameters.AddWithValue("@RemainingQuantity", report.RemainingQuantity);

                newId = Convert.ToInt32(command.ExecuteScalar());
            }

            return newId;
        }

        // Cập nhật thông tin báo cáo tồn kho
        public bool UpdateInventoryReport(InventoryReport report)
        {
            int rowsAffected;
            using (var connection = new NpgsqlConnection(ConnectionHelper.BuildConnectionString()))
            {
                connection.Open();

                // Check if the report exists
                var checkSql = "SELECT COUNT(*) FROM baocaotonkho WHERE baocaotonid = @ReportID";
                var checkCommand = new NpgsqlCommand(checkSql, connection);
                checkCommand.Parameters.AddWithValue("@ReportID", report.ReportID);
                var reportExists = (long)checkCommand.ExecuteScalar() > 0;

                if (!reportExists)
                {
                    return false; // Report does not exist, return false
                }

                var sql = @"
                UPDATE baocaotonkho
                SET ngaybaocao = @ReportDate, nguyenlieuid = @IngredientID, soluongconlai = @RemainingQuantity
                WHERE baocaotonid = @ReportID";

                var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ReportDate", report.ReportDate);
                command.Parameters.AddWithValue("@IngredientID", report.IngredientID);
                command.Parameters.AddWithValue("@RemainingQuantity", report.RemainingQuantity);
                command.Parameters.AddWithValue("@ReportID", report.ReportID);

                rowsAffected = command.ExecuteNonQuery();
            }

            return rowsAffected > 0;
        }

        // Xóa báo cáo tồn kho theo ID
        public void RemoveInventoryReportById(int reportId)
        {
            using (var connection = new NpgsqlConnection(ConnectionHelper.BuildConnectionString()))
            {
                connection.Open();

                // Check if the report exists
                var checkSql = "SELECT COUNT(*) FROM baocaotonkho WHERE baocaotonid = @ReportID";
                var checkCommand = new NpgsqlCommand(checkSql, connection);
                checkCommand.Parameters.AddWithValue("@ReportID", reportId);
                var reportExists = (long)checkCommand.ExecuteScalar() > 0;

                if (!reportExists)
                {
                    return;
                }

                var sql = "DELETE FROM baocaotonkho WHERE baocaotonid = @ReportID";
                var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ReportID", reportId);

                command.ExecuteNonQuery();
            }
        }
    }
}