using MarketingCoinBase.IRepositories;
using MarketingCoinBase.Models;

namespace MarketingCoinBase.Repositories
{
    public class ServeProdsRepository : GenericRepository<ServeProds>, IServeProdsRepository
    {
        private readonly CoinBaseDB _context;
        public ServeProdsRepository(CoinBaseDB context) : base(context)
        {
            _context = context; 
        }
    }
}
