using OrgOffering.Models;

namespace OrgOffering.Repository
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        Service GetMostRecentService();
    }
}
