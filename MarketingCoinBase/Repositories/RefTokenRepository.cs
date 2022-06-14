using MarketingCoinBase.IRepositories;
using MarketingCoinBase.Models;

namespace MarketingCoinBase.Repositories
{
    public class RefTokenRepository : GenericRepository<RefreshToken>, IRefTokenRepository
    {
        private readonly CoinBaseDB _context;
        public RefTokenRepository(CoinBaseDB context) : base(context)
        {
            _context = context;
        }
    }
}
