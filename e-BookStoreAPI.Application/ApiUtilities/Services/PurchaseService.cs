using global::eBookStoreAPI.Application.ApiUtilities.Interfaces;
using global::eBookStoreAPI.Application.ApiUtilities.Shared;
using global::eBookStoreAPI.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace eBookStoreAPI.Application.ApiUtilities.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseHistoryRepository _purchaseHistoryRepository;

        public PurchaseService(IPurchaseHistoryRepository purchaseHistoryRepository)
        {
            _purchaseHistoryRepository = purchaseHistoryRepository;
        }

        public async Task AddPurchaseAsync(PurchaseHistory purchase)
        {
            await _purchaseHistoryRepository.AddPurchaseAsync(purchase);
        }

        public async Task<IEnumerable<PurchaseHistory>> GetPurchaseHistoryByUserIdAsync(int userId, int pageNumber, int pageSize)
        {
            return await _purchaseHistoryRepository.GetPurchaseHistoryByUserIdAsync(userId, pageNumber, pageSize);
        }
    }
}
