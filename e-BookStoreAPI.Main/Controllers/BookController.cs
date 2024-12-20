using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eBookStoreAPI.Application.ApiUtilities.Shared;

using eBookStoreAPI.Domain.Entities;
using eBookStoreAPI.Application.Products.Command.AddBook;
using eBookStoreAPI.Application.Dashboard.Query.SearchBook;

namespace eBookStoreAPI.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BookController> _logger;

    public BookController(IMediator mediator, ILogger<BookController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchBooks(
      [FromQuery] string query,
      [FromQuery] int pageNumber = 1,
      [FromQuery] int pageSize = 10)
    
    {

        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest(new { Message = "Search query cannot be empty." });
        }
        var result = await _mediator.Send(new SearchBookQuery(query, pageNumber, pageSize));

        if(!result.Success)
        {
            _logger.LogInformation("No books found for the search query: {Query}", query);
            return NotFound(result);

        }
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] AddBookCommand command)
    {
        var product = await _mediator.Send(command);

        if (!product.Success)
        {
            _logger.LogWarning("Failed to add book with command: {Command}", command);
            return BadRequest(product);
        }

        _logger.LogInformation("Book with ID {BookId} added successfully.", product.Data!.Id);

        return Ok(product);
    }
}
