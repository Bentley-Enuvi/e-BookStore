﻿using Microsoft.Extensions.Configuration;
using Npgsql;
using eBookStoreAPI.Application.ApiUtilities.Interfaces;
using System.Data;


namespace DataAccess.Repositories;

public class DatabaseContext : IDisposable, IDatabaseContext
{
    private readonly IDbConnection _connection;

    public DatabaseContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        _connection = new NpgsqlConnection(connectionString);
    }

    public IDbConnection Connection => _connection;

    public void Dispose()
    {
        _connection?.Dispose();
    }
}
