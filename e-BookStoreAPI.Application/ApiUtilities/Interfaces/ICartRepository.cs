using eBookStoreAPI.Application.ApiUtilities.Shared;
using eBookStoreAPI.Domain.Entities;

namespace eBookStoreAPI.Application.ApiUtilities.Interfaces
{
    public interface ICartRepository
    {
        Task<int> AddToCartAsync(CartItem cartItem);
        Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(int userId, int pageNumber, int pageSize);
        Task RemoveFromCartAsync(int cartItemId);
    }

}