using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Dapper;
using eBookStoreAPI.Application.ApiUtilities.Interfaces;

namespace eBookStoreAPI.Infrastructure.DataAccess
{


    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<DatabaseInitializer> _logger;

        public DatabaseInitializer(IDbConnection dbConnection, ILogger<DatabaseInitializer> logger)
        {
            _dbConnection = dbConnection;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            _logger.LogInformation("Initializing the database...");

            await CreateUsersTableAsync();
            await CreateBooksTableAsync();
            await CreateCartItemsTableAsync();
            await CreatePurchaseHistoryTableAsync();

            await SeedDataAsync();

            _logger.LogInformation("Database initialization completed.");
        }

        private async Task CreateUsersTableAsync()
        {
            var query = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id SERIAL PRIMARY KEY,
                    Username VARCHAR(100) NOT NULL UNIQUE,
                    PasswordHash VARCHAR(255) NOT NULL,
                    DeletedAt TIMESTAMP,
                    CreatedOn TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    UpdatedOn TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
                );";

            await _dbConnection.ExecuteAsync(query);
            _logger.LogInformation("Users table created or already exists.");
        }

        private async Task CreateBooksTableAsync()
        {
            var query = @"
                CREATE TABLE IF NOT EXISTS Books (
                Id VARCHAR(255) PRIMARY KEY, 
                Title VARCHAR(255) NOT NULL,
                Genre VARCHAR(100) NOT NULL,
                ISBN VARCHAR(20) NOT NULL UNIQUE,
                AuthorName VARCHAR(100) NOT NULL,
                PublishedYear INT NOT NULL,
                CreatedOn TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
            );
                ";

            await _dbConnection.ExecuteAsync(query);
            _logger.LogInformation("Books table created or already exists.");
        }

        private async Task CreateCartItemsTableAsync()
        {
            var query = @"
                CREATE TABLE IF NOT EXISTS CartItems (
                    Id SERIAL PRIMARY KEY,
                    UserId INT NOT NULL,
                    BookId VARCHAR(255) NOT NULL,
                    Quantity INT NOT NULL,
                    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
                    FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE CASCADE
                );";

            await _dbConnection.ExecuteAsync(query);
            _logger.LogInformation("CartItems table created or already exists.");
        }

        private async Task CreatePurchaseHistoryTableAsync()
        {
            var query = @"
                CREATE TABLE IF NOT EXISTS PurchaseHistory (
                    Id SERIAL PRIMARY KEY,
                    UserId INT NOT NULL,
                    BookId VARCHAR(255) NOT NULL,
                    PurchasedOn TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
                    FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE CASCADE
                );";

            await _dbConnection.ExecuteAsync(query);
            _logger.LogInformation("PurchaseHistory table created or already exists.");
        }

        private async Task SeedDataAsync()
        {
            var userQuery = @"
                INSERT INTO Users (Username, PasswordHash)
                VALUES
                ('testuser', 'hashedpassword')
                ON CONFLICT DO NOTHING;";

            var bookQuery = @"
                INSERT INTO Books (Id, Title, Genre, ISBN, AuthorName, PublishedYear)
                VALUES
                ('f8f8b789-be59-4b6c-9a5e-c725e626a6c1', 'Sample Book', 'Fiction', '1234567890', 'Author Name', 2023)
                ON CONFLICT DO NOTHING;";

            await _dbConnection.ExecuteAsync(userQuery);
            await _dbConnection.ExecuteAsync(bookQuery);

            _logger.LogInformation("Seed data inserted.");
        }
    }
}
