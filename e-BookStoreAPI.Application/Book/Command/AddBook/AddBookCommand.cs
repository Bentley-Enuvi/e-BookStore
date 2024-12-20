using eBookStoreAPI.Application.ApiUtilities.Shared;
using MediatR;

namespace eBookStoreAPI.Application.Products.Command.AddBook;
public record AddBookCommand(string Title, string Genre, string ISBN, string AuthorName, int PublishedYear) : IRequest<ApiResponse<BookResponse>>;

