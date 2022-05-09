using System.Threading.Tasks;
using Identity.Api.Data;
using Identity.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<UserEntity> _userEntity;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _userEntity = _dbContext.Set<UserEntity>();
        }
        public async Task<UserEntity> Get(string username, string password)
        {
            return await _userEntity.FirstOrDefaultAsync(a => a.Username == username && a.Password == password);
        }
    }
}