using eBookStoreAPI.Application.ApiUtilities.Shared;
using MediatR;

namespace eBookStoreAPI.Application.Cart.Command.RemoveFromCartCommand;
public record RemoveFromCartCommand(int CartItemId) : IRequest<ApiResponse<bool>>;

