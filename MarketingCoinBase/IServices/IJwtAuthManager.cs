using MarketingCoinBase.DTO.RequestModels;
using MarketingCoinBase.DTO.ResponseModels;
using MarketingCoinBase.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MarketingCoinBase.IServices
{
    public interface IJwtAuthManager
    {
        Task<SignInResponseModel> Authenticate(SignInRequestModel model, string ipAddress);
        Task<SignInResponseModel> RefreshToken(string token, string ipAddress);
        Task<bool> RevokeToken(string token, string ipAddress);
        Task<IEnumerable<Users>> GetAll();
        Task<Users> GetById(long id);

    }
}
