using System.Threading.Tasks;
using Identity.Api.Models;

namespace Identity.Api.Services
{
    public interface IUserService
    {
        Task<SecurityToken> Authenticate(string username, string password);
    }
}