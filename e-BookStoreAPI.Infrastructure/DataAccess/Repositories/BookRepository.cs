using Dapper;
using eBookStoreAPI.Application.ApiUtilities.Interfaces;
using eBookStoreAPI.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Data;

public class BookRepository : IBookRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ILogger<BookRepository> _logger;

    public BookRepository(IDbConnection dbConnection, ILogger<BookRepository> logger)
    {
        _dbConnection = dbConnection;
        _logger = logger;
    }
    public async Task<IEnumerable<Book>> SearchBooksAsync(string query, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var sqlQuery = @"
        SELECT * 
        FROM Books 
        WHERE 
            Title ILIKE '%' || @SearchTerm || '%'
            OR AuthorName ILIKE '%' || @SearchTerm || '%'
            OR CAST(PublishedYear AS TEXT) ILIKE '%' || @SearchTerm || '%'
            OR Genre ILIKE '%' || @SearchTerm || '%'
        ORDER BY Title
        LIMIT @PageSize OFFSET @Offset";

        var parameters = new
        {
            SearchTerm = query,
            PageSize = pageSize,
            Offset = (pageNumber - 1) * pageSize
        };

        return await _dbConnection.QueryAsync<Book>(sqlQuery, parameters);
    }



    public async Task<Book?> GetBookByIdAsync(string bookId)
    {
        var query = "SELECT * FROM Books WHERE Id = @Id;";
        return await _dbConnection.QuerySingleOrDefaultAsync<Book?>(query, new { Id = bookId });
    }

    public async Task<string> AddBookAsync(Book book, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        const string query = @"
            INSERT INTO Books (Id, Title, Genre, ISBN, AuthorName, PublishedYear, CreatedOn)
            VALUES (@Id, @Title, @Genre, @ISBN, @AuthorName, @PublishedYear, @CreatedOn);
        ";

        var result = await _dbConnection.ExecuteAsync(query, book);

        if (result > 0)
        {
            return book.Id;
        }

        return string.Empty;
    }
}

