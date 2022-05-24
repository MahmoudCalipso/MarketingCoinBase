using MarketingCoinBase.Models;
using System.Threading.Tasks;

namespace MarketingCoinBase.IServices
{
    public interface IPartnerServices
    {
        Task AddPartner(Partners partner);
        Task<Partners> GetPartnerByID(long id);
    }
}
