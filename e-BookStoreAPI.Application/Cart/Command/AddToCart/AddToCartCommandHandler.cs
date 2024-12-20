using MediatR;
using eBookStoreAPI.Application.ApiUtilities.Interfaces;
using eBookStoreAPI.Application.ApiUtilities.Shared;

namespace eBookStoreAPI.Application.Products.Command.AddBook;

public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, ApiResponse<int>>
{
    private readonly ICartService _cartService;

    public AddToCartCommandHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<ApiResponse<int>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        var result = await _cartService.AddToCartAsync(request.UserId, request.BookId, request.Quantity);

        if (result <= 0)
        {
            return new ApiResponse<int>
            {
                Data = result,
                Message = "Failed to add item to cart.",
                Success = false
            };
        }

        return new ApiResponse<int>
        {
            Data = result,
            Message = "Item added to cart successfully.",
            Success = true
        };
    }
}


