using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Identity.Api.Helpers;
using Identity.Api.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SecurityToken = Identity.Api.Models.SecurityToken;

namespace Identity.Api.Services
{
    public class UserService : IUserService
    {
        
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;

        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
        }
        
        
        public async Task<SecurityToken> Authenticate(string username, string password)
        {
            var user = await _userRepository.Get(username, password);
            
            // return null if user not found
            if (user == null)
                return null;
            
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("customernumber", user.CustomerNumber.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(59).AddSeconds(59),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtSecurityToken = tokenHandler.WriteToken(token);

            return new SecurityToken() { AuthToken = jwtSecurityToken };
        }
    }
}