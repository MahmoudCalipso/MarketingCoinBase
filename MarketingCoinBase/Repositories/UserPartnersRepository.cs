using MarketingCoinBase.IRepositories;
using MarketingCoinBase.Models;

namespace MarketingCoinBase.Repositories
{
    public class UserPartnersRepository : GenericRepository<UserPartners>, IUserPartnersRepository
    {
        private readonly CoinBaseDB _context;
        public UserPartnersRepository(CoinBaseDB context) : base(context)
        {
            _context = context;
        }
    }
}
