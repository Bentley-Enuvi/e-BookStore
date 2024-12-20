using eBookStoreAPI.Application.ApiUtilities.Shared;
using MediatR;

namespace eBookStoreAPI.Application.Products.Command.AddBook;
public record AddToCartCommand(int UserId, string BookId, int Quantity) : IRequest<ApiResponse<int>>;

