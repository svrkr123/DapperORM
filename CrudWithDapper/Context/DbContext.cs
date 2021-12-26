using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CrudWithDapper.Context
{
    public static class DbContext
    {
        private static readonly string _sqlConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ToString();
        public static IDbConnection db = new SqlConnection(_sqlConnectionString);
        

        public static int ExecuteNonQuery(string procedureName, DynamicParameters parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();
                return connection.Execute(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public static T ExecuteScalar<T>(string procedureName, DynamicParameters parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();
                return (T) Convert.ChangeType(connection.ExecuteScalar(procedureName, parameters, commandType: CommandType.StoredProcedure), typeof(T));
            }
        }

        public static IEnumerable<T> ExecuteReader<T>(string procedureName, DynamicParameters parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();
                return connection.Query<T>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    } 
}