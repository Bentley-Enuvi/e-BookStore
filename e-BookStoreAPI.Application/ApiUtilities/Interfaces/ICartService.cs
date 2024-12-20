using eBookStoreAPI.Application.ApiUtilities.Shared;
using eBookStoreAPI.Domain.Entities;

namespace eBookStoreAPI.Application.ApiUtilities.Interfaces
{
    public interface ICartService
    {
        Task<int> AddToCartAsync(int UserId, string BookId, int quantity);
        Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(int userId, int pageNumber, int pageSize);
        Task RemoveFromCartAsync(int cartItemId);
    }
}