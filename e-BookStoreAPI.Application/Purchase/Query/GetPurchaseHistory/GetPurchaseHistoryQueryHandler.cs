using MediatR;
using eBookStoreAPI.Application.ApiUtilities.Interfaces;
using eBookStoreAPI.Application.ApiUtilities.Shared;
using eBookStoreAPI.Application.Dashboard.Query.SearchBook;
using eBookStoreAPI.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace eBookStoreAPI.Application.Cart.Query.ViewCart;




    public class GetPurchaseHistoryQueryHandler : IRequestHandler<GetPurchaseHistoryQuery, ApiResponse<IEnumerable<PurchaseHistory>>>
    {
        private readonly IPurchaseService _purchaseService;
        private readonly ILogger<GetPurchaseHistoryQueryHandler> _logger;

        public GetPurchaseHistoryQueryHandler(IPurchaseService purchaseService, ILogger<GetPurchaseHistoryQueryHandler> logger)
        {
            _purchaseService = purchaseService;
            _logger = logger;
        }

        public async Task<ApiResponse<IEnumerable<PurchaseHistory>>> Handle(GetPurchaseHistoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
              
                var purchaseHistory = await _purchaseService.GetPurchaseHistoryByUserIdAsync(request.UserId, request.PageNumber, request.PageSize);

                
                return new ApiResponse<IEnumerable<PurchaseHistory>>
                {
                    Data = purchaseHistory,
                    Message = "Purchase history retrieved successfully.",
                    Success = true,
                   
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving purchase history: {ex.Message}");
                return new ApiResponse<IEnumerable<PurchaseHistory>>
                {
                    Data = null,
                    Message = "Failed to retrieve purchase history.",
                    Success = false
                };
            }
        }
    }



