using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Transaction.Api.Data;
using Transaction.Api.Data.Entities;

namespace Transaction.Api.Repositories
{
    public class AccountSummaryRepository : IAccountSummaryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<AccountSummaryEntity> _accountSummaryEntity;

        public AccountSummaryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _accountSummaryEntity = _dbContext.Set<AccountSummaryEntity>();
        }

        public async Task<AccountSummaryEntity> Read(int accountNumber)
        {
            return await _accountSummaryEntity.AsNoTracking()
                .FirstOrDefaultAsync(e => e.AccountNumber == accountNumber);
        }
    }
}