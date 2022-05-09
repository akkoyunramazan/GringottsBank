using System.Collections.Generic;
using System.Threading.Tasks;
using Account.Api.Entities;

namespace Account.Api.Repositories
{
    public interface IAccountRepository
    {
        Task<AccountSummaryEntity> Read(int accountNumber);

        Task<List<AccountSummaryEntity>> ReadAllAccountByCustomer(int customerNumber);
        
        Task<AccountSummaryEntity> Create(AccountSummaryEntity accountInformation);
    }
}