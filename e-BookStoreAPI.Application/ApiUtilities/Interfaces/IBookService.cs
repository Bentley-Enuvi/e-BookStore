using eBookStoreAPI.Application.ApiUtilities.Shared;


namespace eBookStoreAPI.Application.ApiUtilities.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<eBookStoreAPI.Domain.Entities.Book>> SearchBooksAsync(string query, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<string> AddBookAsync(string title, string genre, string isbn, string authorName, int publishedYear, CancellationToken cancellationToken);
    }
}