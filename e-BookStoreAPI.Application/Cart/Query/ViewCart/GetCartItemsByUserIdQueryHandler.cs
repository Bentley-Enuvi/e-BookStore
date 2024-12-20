using MediatR;
using eBookStoreAPI.Application.ApiUtilities.Interfaces;
using eBookStoreAPI.Application.ApiUtilities.Shared;
using eBookStoreAPI.Application.Dashboard.Query.SearchBook;
using eBookStoreAPI.Domain.Entities;

namespace eBookStoreAPI.Application.Cart.Query.ViewCart;



public class GetCartItemsByUserIdQueryHandler : IRequestHandler<GetCartItemsByUserIdQuery, ApiResponse<IEnumerable<CartItem>>>
{
    private readonly ICartService _cartService;

    public GetCartItemsByUserIdQueryHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<ApiResponse<IEnumerable<CartItem>>> Handle(GetCartItemsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _cartService.GetCartItemsByUserIdAsync(request.UserId, request.PageNumber, request.PageSize);

        if (result == null || !result.Any())
        {
            return new ApiResponse<IEnumerable<CartItem>>
            {
                Data = result,
                Message = "No items found in the cart.",
                Success = false
            };
        }

        return new ApiResponse<IEnumerable<CartItem>>
        {
            Data = result,
            Message = "Cart items retrieved successfully.",
            Success = true
        };
    }
}

