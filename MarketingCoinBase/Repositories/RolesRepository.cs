using MarketingCoinBase.IRepositories;
using MarketingCoinBase.Models;

namespace MarketingCoinBase.Repositories
{
    public class RolesRepository : GenericRepository<Roles>, IRolesRepository
    {
        private readonly CoinBaseDB _context;
        public RolesRepository(CoinBaseDB context):base(context)
        {
            _context = context;
        }
    }
}
