using eBookStoreAPI.Application.ApiUtilities.Shared;
using eBookStoreAPI.Domain.Entities;

namespace eBookStoreAPI.Application.ApiUtilities.Interfaces
{

    public interface IPurchaseService
    {
        Task AddPurchaseAsync(PurchaseHistory purchase);
        Task<IEnumerable<PurchaseHistory>> GetPurchaseHistoryByUserIdAsync(int userId, int pageNumber, int pageSize);
    }

}