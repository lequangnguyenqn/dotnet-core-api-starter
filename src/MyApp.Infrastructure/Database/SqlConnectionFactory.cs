using MyApp.Application.Configuration.Data;
using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace MyApp.Infrastructure.Database
{
    public class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        // To detect redundant calls
        private bool _disposed = false;
        private readonly string _connectionString;
        private IDbConnection _connection;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetOpenConnection()
        {
            if (_connection == null || this._connection.State != ConnectionState.Open)
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
            }

            return _connection;
        }

        public string GetSQLPaging(int currentPage, int pageSize)
        {
            return $"OFFSET {(currentPage - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY; ";
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing && _connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Dispose();
            }

            _disposed = true;
        }

        ~SqlConnectionFactory()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }
    }
}
