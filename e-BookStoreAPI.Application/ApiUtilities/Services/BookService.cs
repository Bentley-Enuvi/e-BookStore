using eBookStoreAPI.Domain.Entities;
using global::eBookStoreAPI.Application.ApiUtilities.Interfaces;
using global::eBookStoreAPI.Application.ApiUtilities.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBookStoreAPI.Application.ApiUtilities.Services;

  

    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

    public async Task<IEnumerable<eBookStoreAPI.Domain.Entities.Book>> SearchBooksAsync(string query, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _bookRepository.SearchBooksAsync(query, pageNumber, pageSize, cancellationToken);
    }

    public async Task<string> AddBookAsync(string title, string genre, string isbn, string authorName, int publishedYear, CancellationToken cancellationToken)
    {
        var newBook = new eBookStoreAPI.Domain.Entities.Book
        {
            Id = Guid.NewGuid().ToString(),
            Title = title,
            Genre = genre,
            ISBN = isbn,
            AuthorName = authorName,
            PublishedYear = publishedYear,
            CreatedOn = DateTime.UtcNow
        };

        return await _bookRepository.AddBookAsync(newBook, cancellationToken);
    }
}






