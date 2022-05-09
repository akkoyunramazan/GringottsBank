using Account.Api.Models;

namespace Account.Api.Services
{
    public interface IIdentityService
    {
        IdentityModel GetIdentity();
    }
}