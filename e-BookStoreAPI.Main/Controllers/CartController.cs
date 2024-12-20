using eBookStoreAPI.Application.ApiUtilities.Shared;
using eBookStoreAPI.Application.Cart.Command.RemoveFromCartCommand;
using eBookStoreAPI.Application.Cart.Query.ViewCart;
using eBookStoreAPI.Application.Products.Command.AddBook;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eCartStoreAPI.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CartController> _logger;

    public CartController(IMediator mediator, ILogger<CartController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("View-Cart")]
    public async Task<IActionResult> ViewCarts(
      [FromQuery] int userId,
      [FromQuery] int pageNumber = 1,
      [FromQuery] int pageSize = 10)
    
    {
    
        var result = await _mediator.Send(new GetCartItemsByUserIdQuery(userId, pageNumber, pageSize));

        if(!result.Success)
        {
            _logger.LogInformation("No Carts found for the user: {userId}", userId);
            return NotFound(result);

        }
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command)
    {

        var result = await _mediator.Send(command);

        if (!result.Success)
        {
            _logger.LogWarning("Failed to add Cart with command: {Command}", command);
            return BadRequest(result);
        }

        _logger.LogInformation("Cart with ID {CartId} added successfully.", result.Data);

        return Ok(result);
    }


    [HttpDelete("{cartItemId}")]
    public async Task<IActionResult> RemoveFromCart([FromRoute] int cartItemId)
    {
        var command = new RemoveFromCartCommand(cartItemId);
        var result = await _mediator.Send(command);

        if (!result.Success)
        {
            _logger.LogWarning("Failed to remove Cart item with ID {CartItemId}", cartItemId);
            return BadRequest(result);
        }

        _logger.LogInformation("Cart item with ID {CartItemId} removed successfully.", cartItemId);
        return Ok(result);
    }
    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout([FromBody] CheckoutCommand command)
    {
       
        var response = await _mediator.Send(command);

        if (!response.Success)
        {
            _logger.LogWarning("Checkout failed for User {UserId}. Reason: {Message}", command.UserId, response.Message);
            return BadRequest(response);
        }

        _logger.LogInformation("Checkout successful for User {UserId}, Book {BookId}", command.UserId, command.BookId);
        return Ok(response);
    }


    [HttpGet("purchase-history")]
    public async Task<IActionResult> GetPurchaseHistory([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var query = new GetPurchaseHistoryQuery(userId, pageNumber, pageSize);

        
        var result = await _mediator.Send(query);

        if (!result.Success)
        {
            _logger.LogWarning("Failed to retrieve purchase history for user {UserId}", userId);
            return NotFound(result);
        }

        return Ok(result);
    }
}
