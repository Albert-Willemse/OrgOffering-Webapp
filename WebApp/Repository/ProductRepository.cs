using OrgOffering.Data;
using OrgOffering.Models;
using System.Linq;

namespace OrgOffering.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(OrgOfferingDBContext context) : base(context)
        {

        }

    }
}
