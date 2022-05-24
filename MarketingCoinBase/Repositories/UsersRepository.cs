using MarketingCoinBase.IRepositories;
using MarketingCoinBase.Models;

namespace MarketingCoinBase.Repositories
{
    public class UsersRepository : GenericRepository<Users>, IUsersRepository
    {
        private readonly CoinBaseDB _context;
        public UsersRepository(CoinBaseDB context) : base(context)
        {
            _context = context; 
        }
    }
}
