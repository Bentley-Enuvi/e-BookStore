using System.Data;

namespace eBookStoreAPI.Application.ApiUtilities.Interfaces
{
    public interface IDatabaseContext
    {
        IDbConnection Connection { get; }

        void Dispose();
    }
}