using MediatR;
using eBookStoreAPI.Application.ApiUtilities.Interfaces;
using eBookStoreAPI.Application.ApiUtilities.Shared;
using eBookStoreAPI.Application.Dashboard.Query.SearchBook;

namespace eBookStoreAPI.Application.Book.Query.eBookStoreAPI.SearchBook;




public class SearchBookQueryHandler : IRequestHandler<SearchBookQuery, ApiResponse<IEnumerable<BookResponse>>>
{
    private readonly IBookService _bookService;

    public SearchBookQueryHandler(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<ApiResponse<IEnumerable<BookResponse>>> Handle(SearchBookQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookService.SearchBooksAsync(request.Query, request.PageNumber, request.PageSize, cancellationToken);

        if (!books.Any())
        {
            return new ApiResponse<IEnumerable<BookResponse>>
            {

                Data = null,
                Message = "No books found for the provided query.",
                Success = false
            };
        };


        var bookResponses = books.Select(book => new BookResponse
        {
            Id = book.Id,
            Title = book.Title,
            Genre = book.Genre,
            ISBN = book.ISBN,
            AuthorName = book.AuthorName,
            PublishedYear = book.PublishedYear,
            CreatedOn = book.CreatedOn
        });

       
        return new ApiResponse<IEnumerable<BookResponse>>
        {
            Data = bookResponses,
            Message = "Books retrieved successfully.",
            Success = true
        };
    }

}

