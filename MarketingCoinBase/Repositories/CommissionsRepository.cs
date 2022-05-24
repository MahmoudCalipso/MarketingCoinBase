using MarketingCoinBase.IRepositories;
using MarketingCoinBase.Models;

namespace MarketingCoinBase.Repositories
{
    public class CommissionsRepository : GenericRepository<Commissions>, ICommissionsRepository
    {
        private readonly CoinBaseDB _context;

        public CommissionsRepository(CoinBaseDB context):base(context)
        {
            _context = context;
        }
    }
}
