using MediatR;
using eBookStoreAPI.Application.ApiUtilities.Shared;

namespace eBookStoreAPI.Application.Dashboard.Query.SearchBook;

public record SearchBookQuery(string Query, int PageNumber = 1, int PageSize = 10) : IRequest<ApiResponse<IEnumerable<BookResponse>>>;
