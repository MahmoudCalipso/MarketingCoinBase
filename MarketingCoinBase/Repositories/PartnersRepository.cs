using MarketingCoinBase.IRepositories;
using MarketingCoinBase.Models;

namespace MarketingCoinBase.Repositories
{
    public class PartnersRepository : GenericRepository<Partners>, IPartnersRepository
    {
        private readonly CoinBaseDB _context;
        public PartnersRepository(CoinBaseDB context) : base(context)
        {
            _context = context; 
        }
    }
}
