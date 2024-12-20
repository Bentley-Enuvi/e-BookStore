using eBookStoreAPI.Domain.Entities;


namespace eBookStoreAPI.Application.ApiUtilities.Interfaces;
public interface IBookRepository
{
    Task<IEnumerable<eBookStoreAPI.Domain.Entities.Book>> SearchBooksAsync(string query, int pageNumber, int pageSize, CancellationToken cancellationToken);
   Task<eBookStoreAPI.Domain.Entities.Book?> GetBookByIdAsync(string bookId);
    Task<string> AddBookAsync(eBookStoreAPI.Domain.Entities.Book book, CancellationToken cancellationToken);
}