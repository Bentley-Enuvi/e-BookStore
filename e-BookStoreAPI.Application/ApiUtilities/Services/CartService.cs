using global::eBookStoreAPI.Application.ApiUtilities.Interfaces;
using global::eBookStoreAPI.Application.ApiUtilities.Shared;
using global::eBookStoreAPI.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace eBookStoreAPI.Application.ApiUtilities.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
       
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<int> AddToCartAsync(int UserId, string BookId , int quantity)
        {

            var cartRequest = new CartItem
            {
                BookId = BookId,
                Quantity = quantity,
                UserId = UserId
            };
            return await _cartRepository.AddToCartAsync(cartRequest);
            
                
                ;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(int userId, int pageNumber, int pageSize)
        {
            return await _cartRepository.GetCartItemsByUserIdAsync(userId, pageNumber, pageSize);
        }

        public async Task RemoveFromCartAsync(int cartItemId)
        {
            await _cartRepository.RemoveFromCartAsync(cartItemId);
        }
    }
}

