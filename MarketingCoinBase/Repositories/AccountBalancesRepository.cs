using MarketingCoinBase.IRepositories;
using MarketingCoinBase.Models;

namespace MarketingCoinBase.Repositories
{
    public class AccountBalancesRepository : GenericRepository<AccountBalances>, IAccountBalancesRepository
    {
        private readonly CoinBaseDB _context;
        public AccountBalancesRepository(CoinBaseDB context) : base(context)
        {
            _context = context; 
        }
    }
}
