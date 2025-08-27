using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace ToDoConsoleApp
{
    public class ToDoService
    {

        SqlConnectionStringBuilder sql = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "test1",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true
        };


        public void  ReadAll()
        {
            SqlConnection conn = new SqlConnection(sql.ConnectionString);
            conn.Open();
            string query = @"SELECT [Id]
      ,[Title]
      ,[Description]
      ,[IsComplete]
      ,[DueDate]
  FROM [dbo].[ToDoItems]";
           SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            Console.WriteLine("\nID | Title | Description | Complete | DueDate");
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["Id"]} | {row["Title"]} | {row["Description"]} | {((bool)row["IsComplete"] ? "Yes" : "No")} | {row["DueDate"]}");
            }
           
        }

        public void Create(string title, string description, DateTime? dueDate)
        {
            SqlConnection conn = new SqlConnection(sql.ConnectionString);
            conn.Open();
            string query = @"
                INSERT INTO dbo.ToDoItems (Title, Description, IsComplete, DueDate)
                VALUES (@title, @description, 0, @dueDate)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@description", description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@dueDate", dueDate ?? (object)DBNull.Value);
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Added successfully." : "Insert failed.";
            Console.WriteLine(message);
        }

        public void Update(int id, string title, string description, bool isComplete, DateTime? dueDate)
        {
            SqlConnection conn = new SqlConnection(sql.ConnectionString);
            conn.Open();
            string query = @"
                UPDATE dbo.ToDoItems
                SET Title = @title,
                    Description = @description,
                    IsComplete = @isComplete,
                    DueDate = @dueDate
                WHERE Id = @id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@description", description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@isComplete", isComplete);
            cmd.Parameters.AddWithValue("@dueDate", dueDate ?? (object)DBNull.Value);
            var result = cmd.ExecuteNonQuery();
            Console.WriteLine(result > 0 ? "Updated successfully." : "Update failed.");
        }

        public void Delete(int id)
        {
            SqlConnection conn = new SqlConnection(sql.ConnectionString);
            conn.Open();
            string query = "DELETE FROM dbo.ToDoItems WHERE Id = @id";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            var result = cmd.ExecuteNonQuery();
            Console.WriteLine(result > 0 ? "Deleted successfully." : "Delete failed.");
        }
    }
}
