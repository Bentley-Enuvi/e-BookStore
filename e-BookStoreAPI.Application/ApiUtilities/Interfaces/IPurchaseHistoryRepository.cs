using eBookStoreAPI.Domain.Entities;

namespace eBookStoreAPI.Application.ApiUtilities.Interfaces;

public interface IPurchaseHistoryRepository
{
    Task AddPurchaseAsync(PurchaseHistory purchase);
    Task<IEnumerable<PurchaseHistory>> GetPurchaseHistoryByUserIdAsync(int userId, int pageNumber, int pageSize);
}
