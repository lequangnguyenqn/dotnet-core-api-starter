using Microsoft.Data.Sqlite;
using MyApp.Application.Configuration.Data;
using System;
using System.Data;

namespace MyApp.IntegrationTests.Configuration
{
    public class SqliteConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        // To detect redundant calls
        private bool _disposed = false;
        private readonly string _connectionString;
        private IDbConnection _connection;

        public SqliteConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetOpenConnection()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                _connection = new SqliteConnection(_connectionString);
                _connection.Open();
            }

            return _connection;
        }

        public string GetSQLPaging(int currentPage, int pageSize)
        {
            return $"LIMIT {pageSize} OFFSET {(currentPage - 1) * pageSize} ; ";
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

        ~SqliteConnectionFactory()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }
    }
}
