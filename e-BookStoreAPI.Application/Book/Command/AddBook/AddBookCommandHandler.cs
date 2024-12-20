using MediatR;
using eBookStoreAPI.Application.ApiUtilities.Interfaces;
using eBookStoreAPI.Application.ApiUtilities.Shared;

namespace eBookStoreAPI.Application.Products.Command.AddBook;

public class AddBookCommandHandler : IRequestHandler<AddBookCommand, ApiResponse<BookResponse>>
{
    private readonly IBookService _bookService;

    public AddBookCommandHandler(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<ApiResponse<BookResponse>> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        var result = await _bookService.AddBookAsync(
            request.Title,
            request.Genre,
            request.ISBN,
            request.AuthorName,
            request.PublishedYear,
            cancellationToken
        );

        if (string.IsNullOrEmpty(result))
        {
            return new ApiResponse<BookResponse>
            {
                Data = null,
                Message = "Failed to add book.",
                Success = false
            };
        }

        return new ApiResponse<BookResponse>
        {
            Data = new BookResponse
            {
                Id = result,
                Title = request.Title,
                Genre = request.Genre,
                ISBN = request.ISBN,
                AuthorName = request.AuthorName,
                PublishedYear = request.PublishedYear,
                CreatedOn = DateTime.UtcNow
            },
            Message = "Book added successfully.",
            Success = true
        };
    }
}


