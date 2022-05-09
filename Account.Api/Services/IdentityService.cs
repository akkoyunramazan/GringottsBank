using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Account.Api.Models;
using Microsoft.AspNetCore.Http;

namespace Account.Api.Services
{
    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public IdentityModel GetIdentity()
        {
            string authorizationHeader = _context.HttpContext.Request.Headers["Authorization"];

            if (authorizationHeader != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = authorizationHeader.Split(" ")[1];
                var parsedToken = tokenHandler.ReadJwtToken(token);

                var customer = parsedToken.Claims
                    .FirstOrDefault(c => c.Type == "customernumber");

                return new IdentityModel() {
                    CustomerNumber = Convert.ToInt32(customer.Value)
                };
            }

            throw new ArgumentNullException("customernumber");
        }
    }
}