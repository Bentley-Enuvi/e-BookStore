using Dapper;
using Microsoft.Extensions.Logging;
using eBookStoreAPI.Domain.Entities;
using eBookStoreAPI.Application.ApiUtilities.Interfaces;
using System.Data;

namespace eBookStoreAPI.Infrastructure.DataAccess.Repositories
{

 
    public class PurchaseHistoryRepository : IPurchaseHistoryRepository
    {
        private readonly IDbConnection _dbConnection;

        public PurchaseHistoryRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task AddPurchaseAsync(PurchaseHistory purchase)
        {
            var query = "INSERT INTO PurchaseHistory (UserId, BookId, PurchasedOn) VALUES (@UserId, @BookId, @PurchasedOn);";
            await _dbConnection.ExecuteAsync(query, purchase);
        }

        public async Task<IEnumerable<PurchaseHistory>> GetPurchaseHistoryByUserIdAsync(int userId, int pageNumber, int pageSize)
        {
            
            var query = @"
                SELECT * 
                FROM PurchaseHistory
                WHERE UserId = @UserId
                ORDER BY PurchasedOn DESC
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY;
            ";

            var parameters = new
            {
                UserId = userId,
                Offset = (pageNumber - 1) * pageSize,
                PageSize = pageSize
            };

            return await _dbConnection.QueryAsync<PurchaseHistory>(query, parameters);
        }

       
        public async Task<int> GetPurchaseHistoryCountAsync(int userId)
        {
            var query = "SELECT COUNT(*) FROM PurchaseHistory WHERE UserId = @UserId;";
            return await _dbConnection.ExecuteScalarAsync<int>(query, new { UserId = userId });
        }
    }
}
