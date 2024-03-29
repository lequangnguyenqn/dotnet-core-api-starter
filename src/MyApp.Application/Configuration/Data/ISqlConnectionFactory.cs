﻿using System.Data;

namespace MyApp.Application.Configuration.Data
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();

        string GetSQLPaging(int currentPage, int pageSize);
    }
}
