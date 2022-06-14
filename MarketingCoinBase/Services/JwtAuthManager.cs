using MarketingCoinBase.DTO.RequestModels;
using MarketingCoinBase.DTO.ResponseModels;
using MarketingCoinBase.IRepositories;
using MarketingCoinBase.IServices;
using MarketingCoinBase.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MarketingCoinBase.Services
{
    public class JwtAuthManager : IJwtAuthManager
    {
        private readonly IUnitOfWork _context;
        private readonly IConfiguration _config;
        public JwtAuthManager(IUnitOfWork context , IConfiguration config )
        {
            _context = context;
            _config = config;

        }
    
        public async Task<SignInResponseModel> Authenticate(SignInRequestModel model, string ipAddress)
        {
            var user = await _context.UsersRepository.FindSingleAsync(x => x.email == model.email 
                                                                        && x.password == model.password);
            if (user == null) return null;
            var jwtToken = GenerateJwtToken(user, ipAddress);
            var refreshToken =  GenerateRefreshToken();
            RefreshToken refToken = new RefreshToken();
            refToken.Token = refreshToken;
            refToken.Created = DateTime.UtcNow;
            refToken.Expires = DateTime.UtcNow.AddDays(Convert.ToInt32(_config["jwtTokenConfig:AccessTokenExpiration"]));
            refToken.userID = user.userID;
           // user.RefreshTokens.Add(refToken);
            await _context.RefTokenRepository.AddAsync(refToken);
            await  _context.SaveCompletedAsync();
            return new SignInResponseModel()
            {
                userId = user.userID,
                username = user.username,
                email = user.email,
                role = Convert.ToString(user.roleID),
                token = jwtToken,
                RefreshToken = GenerateRefreshToken()
            };

        }

        public async  Task<IEnumerable<Users>> GetAll()
        {
            return await _context.UsersRepository.GetAllAsync();
        }

        public async Task<Users> GetById(long id)
        {
            return await _context.UsersRepository.GetByIdAsync(id);
        }

        public async Task<SignInResponseModel> RefreshToken(string token, string ipAddress)
        {
            var user = await _context.UsersRepository.FindSingleAsync(u => u.RefreshTokens.Any(x=> x.Token == token));

            // return null if no user found with token
            if (user == null) return null;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return null if token is no longer active
            if (!refreshToken.IsActive) return null;

            // replace old refresh token with a new one and save
            var newRefreshToken = GenerateRefreshToken();
            RefreshToken refToken = new RefreshToken();

            refToken.Revoked = DateTime.UtcNow;
            refToken.RevokedByIp = ipAddress;
            refToken.ReplacedByToken = newRefreshToken;
            refToken.userID = user.userID;
             _context.RefTokenRepository.Update(refToken);
            await _context.SaveCompletedAsync();

            // generate new jwt
            var jwtToken = GenerateJwtToken(user, ipAddress);

            return new SignInResponseModel()
            {
                userId = user.userID,
                username = user.username,
                email = user.email,
                role = Convert.ToString(user.roleID),
                token = jwtToken,
                RefreshToken = GenerateRefreshToken()
            };
        }

        public async Task<bool> RevokeToken(string token, string ipAddress)
        {
            var user = await _context.UsersRepository.FindSingleAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            // return false if no user found with token
            if (user == null) return false;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return false if token is not active
            if (!refreshToken.IsActive) return false;
            RefreshToken refToken = new RefreshToken();
            // revoke token and save
            refToken.Revoked = DateTime.UtcNow;
            refToken.RevokedByIp = ipAddress;
            _context.RefTokenRepository.Update(refToken);
            await _context.SaveCompletedAsync();

            return true;
        }
        private string GenerateJwtToken(Users user, string ipAdress)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Role, Convert.ToString(user.roleID)),
                new Claim(ClaimTypes.MobilePhone , user.phone),
                new Claim(ClaimTypes.System , ipAdress)
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwtTokenConfig:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(Convert.ToInt32(_config["jwtTokenConfig:AccessTokenExpiration"])),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
         
        }
        private string GenerateRefreshToken()
        {
            using var rng = RandomNumberGenerator.Create();
            var randomBytes = new byte[64];
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);

        }

    }
    
}
