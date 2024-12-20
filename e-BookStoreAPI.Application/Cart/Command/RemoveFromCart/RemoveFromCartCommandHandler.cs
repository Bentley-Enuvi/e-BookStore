using MediatR;
using eBookStoreAPI.Application.ApiUtilities.Interfaces;
using eBookStoreAPI.Application.ApiUtilities.Shared;

namespace eBookStoreAPI.Application.Cart.Command.RemoveFromCartCommand;

public class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand, ApiResponse<bool>>
{
    private readonly ICartService _cartService;

    public RemoveFromCartCommandHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<ApiResponse<bool>> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
    {
        await _cartService.RemoveFromCartAsync(request.CartItemId);

        return new ApiResponse<bool>
        {
            Data = true,
            Message = "Item removed from cart successfully.",
            Success = true
        };
    }
}


