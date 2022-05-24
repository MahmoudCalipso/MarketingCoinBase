using MarketingCoinBase.IRepositories;
using MarketingCoinBase.Models;
using System;
using System.Threading.Tasks;

namespace MarketingCoinBase.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly CoinBaseDB _context;
        private bool disposed = false;
        private IAccountBalancesRepository _accountBalancesRepository;
        private ICommissionsRepository _commissionsRepository;
        private IPartnersRepository _partnersRepository;
        private IRolesRepository _rolesRepository;
        private IServeProdsRepository _serveProdsRepository;
        private IUserPartnersRepository _userPartnersRepository;
        private IUsersRepository _usersRepository;
        public UnitOfWork(CoinBaseDB context)
        {
            _context = context;
        }

        public IAccountBalancesRepository AccountBalancesRepository => 
            _accountBalancesRepository ??= new AccountBalancesRepository(_context);

        public ICommissionsRepository CommissionsRepository =>
           _commissionsRepository ??= new CommissionsRepository(_context);

        public IPartnersRepository PartnersRepository => 
            _partnersRepository ??= new PartnersRepository(_context);

        public IRolesRepository RolesRepository =>
           _rolesRepository ??= new RolesRepository(_context);

        public IServeProdsRepository ServeProdsRepository =>
            _serveProdsRepository ??= new ServeProdsRepository(_context);

        public IUserPartnersRepository UserPartnersRepository =>
            _userPartnersRepository ??= new UserPartnersRepository(_context);

        public IUsersRepository UsersRepository =>
            _usersRepository ??= new UsersRepository(_context);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }
        public async Task<bool> SaveCompletedAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
