using AnagramSolver.BusinessLogic.Utilities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Repository.Helpers
{
    public static class StoredProcedure
    {
        public static void RemoveTableData(string tableName)
        {
            var settings = new Settings()
            {
                ConnectionString = "Server=.;Database=AnagramSolver;Trusted_Connection=True;"
            };

            IOptions<Settings> options = Options.Create(settings);

            var sqlConnection = new SqlConnection(options.Value.ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "dbo.RemoveRecords";
            sqlCommand.Parameters.Add(new SqlParameter("@TableName", tableName));
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}

//CREATE PROCEDURE dbo.RemoveRecords 
//            @TableName NVARCHAR(128) 
//            AS
//            BEGIN 
//            SET NOCOUNT ON;
//DECLARE @Sql NVARCHAR(MAX);

//SET @Sql = N'Delete FROM ' + QUOTENAME(@TableName)

//            EXECUTE sp_executesql @Sql

//            END 
