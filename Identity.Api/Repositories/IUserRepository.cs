using System.Threading.Tasks;
using Identity.Api.Entities;

namespace Identity.Api.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> Get(string username, string password);

    }
}