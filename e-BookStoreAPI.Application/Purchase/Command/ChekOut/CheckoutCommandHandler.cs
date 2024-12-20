using eBookStoreAPI.Application.ApiUtilities.Interfaces;
using eBookStoreAPI.Application.ApiUtilities.Shared;
using eBookStoreAPI.Application.Products.Command.AddBook;
using eBookStoreAPI.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eBookStoreAPI.Application.Cart.Command.Checkout
{
    public class CheckoutCommandHandler : IRequestHandler<CheckoutCommand, ApiResponse<bool>>
    {
        private readonly IPurchaseHistoryRepository _purchaseHistoryRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<CheckoutCommandHandler> _logger;

        public CheckoutCommandHandler(
            IPurchaseHistoryRepository purchaseHistoryRepository,
            IBookRepository bookRepository,
            ILogger<CheckoutCommandHandler> logger)
        {
            _purchaseHistoryRepository = purchaseHistoryRepository;
            _bookRepository = bookRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> Handle(CheckoutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Simulate the checkout process (without actual payment gateway)
                var book = await _bookRepository.GetBookByIdAsync(request.BookId);
                if (book == null)
                {
                    return new ApiResponse<bool>
                    {
                        Data = false,
                        Message = "Book not found.",
                        Success = false
                    };
                }

                // Simulate payment based on payment method
                if (string.IsNullOrEmpty(request.PaymentMethod) || !new[] { "Web", "USSD", "Transfer" }.Contains(request.PaymentMethod))
                {
                    return new ApiResponse<bool>
                    {
                        Data = false,
                        Message = "Invalid payment method.",
                        Success = false
                    };
                }

                // Save purchase history
                var purchaseHistory = new PurchaseHistory
                {
                    UserId = request.UserId,
                    BookId = request.BookId,
                    PurchasedOn = DateTime.UtcNow
                };

                await _purchaseHistoryRepository.AddPurchaseAsync(purchaseHistory);

                _logger.LogInformation($"User {request.UserId} purchased Book {request.BookId} via {request.PaymentMethod} payment.");

                return new ApiResponse<bool>
                {
                    Data = true,
                    Message = "Checkout successful.",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during checkout: {ex.Message}");
                return new ApiResponse<bool>
                {
                    Data = false,
                    Message = "Checkout failed.",
                    Success = false
                };
            }
        }
    }
}
