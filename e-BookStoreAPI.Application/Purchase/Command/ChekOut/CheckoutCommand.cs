using eBookStoreAPI.Application.ApiUtilities.Shared;
using MediatR;

namespace eBookStoreAPI.Application.Products.Command.AddBook;
public record CheckoutCommand(int UserId, string BookId, string PaymentMethod) : IRequest<ApiResponse<bool>>;

