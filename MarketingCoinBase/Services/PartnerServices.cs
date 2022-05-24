using MarketingCoinBase.DTO.RequestModels;
using MarketingCoinBase.IRepositories;
using MarketingCoinBase.IServices;
using MarketingCoinBase.Models;
using System.Threading.Tasks;

namespace MarketingCoinBase.Services
{
    public class PartnerServices : IPartnerServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public PartnerServices (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddPartner(Partners partner)
        {
            await _unitOfWork.PartnersRepository.AddAsync(partner);

        }

        public async Task<Partners> GetPartnerByID(long id)
        {
            return await _unitOfWork.PartnersRepository.GetByIdAsync(id);
        }
    }
}
