using MarketingCoinBase.Models;
using System;
using System.Text.Json.Serialization;

namespace MarketingCoinBase.DTO.ResponseModels
{
    public class SignInResponseModel
    {
        public long userId { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string role { get; set; }
        public bool isVerified { get; set; }
        public string token { get; set; }
        public DateTime expiredAt { get; set; } 
        
      
        public string RefreshToken { get; set; }
        //public SignInResponseModel(Users user, string jwtToken, string refreshToken)
        //{
        //    userId = user.userID;
        //    username = user.username;
        //    email = user.email;
        //    token = jwtToken;
        //    RefreshToken = refreshToken;
        //}

    }
}
