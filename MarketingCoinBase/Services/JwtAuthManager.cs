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
            Console.WriteLine( "Authenticated User {0}", user.username);
            var UserAuth = new Users();
            UserAuth = user;
            var jwtToken = GenerateJwtToken(UserAuth);
            var refreshToken =  GenerateRefreshToken(ipAddress);

            // save refresh token
            user.RefreshTokens.Add(refreshToken);
             _context.UsersRepository.Update(user);
           await  _context.SaveCompletedAsync();

            return new SignInResponseModel(user, jwtToken, refreshToken.Token);

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
            var newRefreshToken = GenerateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);
            _context.UsersRepository.Update(user);
            await _context.SaveCompletedAsync();

            // generate new jwt
            var jwtToken = GenerateJwtToken(user);

            return new SignInResponseModel(user, jwtToken, newRefreshToken.Token);
        }

        public async Task<bool> RevokeToken(string token, string ipAddress)
        {
            var user = await _context.UsersRepository.FindSingleAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            // return false if no user found with token
            if (user == null) return false;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return false if token is not active
            if (!refreshToken.IsActive) return false;

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            _context.UsersRepository.Update(user);
            await _context.SaveCompletedAsync();

            return true;
        }
        private string GenerateJwtToken(Users user)
        {
            Console.WriteLine("User Email {0}", user.email);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["jwtTokenConfig:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserID", user.userID.ToString()),
                    new Claim(ClaimTypes.Name, user.username.ToString()),
                    new Claim(ClaimTypes.Role, user.role.ToString()),   
                    new Claim(ClaimTypes.MobilePhone , user.phone.ToString() )
                }),
                Expires = DateTime.UtcNow.AddDays(Convert.ToInt32(_config["jwtTokenConfig:AccessTokenExpiration"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            using var rng = RandomNumberGenerator.Create();
            var randomBytes = new byte[64];
            rng.GetBytes(randomBytes);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(Convert.ToInt32(_config["jwtTokenConfig:AccessTokenExpiration"])),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

    }
    
}
