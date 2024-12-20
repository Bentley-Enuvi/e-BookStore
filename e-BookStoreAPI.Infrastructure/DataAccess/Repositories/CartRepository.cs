using Dapper;
using Microsoft.Extensions.Logging;
using eBookStoreAPI.Domain.Entities;
using eBookStoreAPI.Application.ApiUtilities.Interfaces;
using System.Data;

namespace eBookStoreAPI.Infrastructure.DataAccess.Repositories
{


    public class CartRepository : ICartRepository
    {
        private readonly IDbConnection _dbConnection;

        public CartRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> AddToCartAsync(CartItem cartItem)
        {
            var query = @"
            INSERT INTO CartItems (UserId, BookId, Quantity) 
            VALUES (@UserId, @BookId, @Quantity)
            RETURNING Id;";

            var result = await _dbConnection.ExecuteScalarAsync<int>(query, cartItem);

            return result;
        }


        public async Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(int userId, int pageNumber, int pageSize)
        {
            var query = @"
        SELECT * FROM CartItems
        WHERE UserId = @UserId
        ORDER BY Id -- or any other column for ordering
        LIMIT @PageSize OFFSET @Offset;";

            var parameters = new
            {
                UserId = userId,
                PageSize = pageSize,
                Offset = (pageNumber - 1) * pageSize
            };

            return await _dbConnection.QueryAsync<CartItem>(query, parameters);
        }


        public async Task RemoveFromCartAsync(int cartItemId)
        {
            var query = "DELETE FROM CartItems WHERE Id = @Id;";
            await _dbConnection.ExecuteAsync(query, new { Id = cartItemId });
        }
    }


}
