using System;
using System.Threading.Tasks;

namespace MarketingCoinBase.IRepositories
{
    public interface IUnitOfWork
    {
        IAccountBalancesRepository AccountBalancesRepository { get; }
        ICommissionsRepository CommissionsRepository { get; }
        IPartnersRepository PartnersRepository { get; }
        IRolesRepository RolesRepository { get; }   
        IServeProdsRepository ServeProdsRepository { get; }
        IUserPartnersRepository UserPartnersRepository { get; } 
        IUsersRepository UsersRepository { get; }
        IRefTokenRepository RefTokenRepository { get; }
        Task<bool> SaveCompletedAsync();
    }
}
