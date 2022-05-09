using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account.Api.Data;
using Account.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Account.Api.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<AccountSummaryEntity> _accountEntity;

        public AccountRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _accountEntity = _dbContext.Set<AccountSummaryEntity>();
        }
        public async Task<AccountSummaryEntity> Read(int accountNumber)
        {
            return await _accountEntity.AsNoTracking()
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task<List<AccountSummaryEntity>> ReadAllAccountByCustomer(int customerNumber)
        {
            var accountList = _dbContext.Accounts.Where(c => c.CustomerNumber == customerNumber);
            return await accountList.ToListAsync();
        }

        public async Task<AccountSummaryEntity> Create(AccountSummaryEntity accountInformation)
        {
            var account = await _accountEntity.AddAsync(accountInformation);
            await _dbContext.SaveChangesAsync();
            return accountInformation;
        }
    }
}