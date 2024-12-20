using MediatR;
using eBookStoreAPI.Application.ApiUtilities.Shared;
using eBookStoreAPI.Domain.Entities;

namespace eBookStoreAPI.Application.Cart.Query.ViewCart;

public record GetCartItemsByUserIdQuery(int UserId , int PageNumber = 1, int PageSize = 10) : IRequest<ApiResponse<IEnumerable<CartItem>>>;
