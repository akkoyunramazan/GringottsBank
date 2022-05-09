using System.Collections.Generic;
using System.Threading.Tasks;
using Account.Api.Entities;
using Account.Api.Models;

namespace Account.Api.Services
{
    public interface IAccountService
    {
        Task<AccountResult> CreateAccount(AccountModel accountInformation);

        Task<AccountResult> GetAccountByAccountNumber(int accountNumber);

        Task<List<AccountResult>> ReadAllAccountByCustomer(int customerNumber);
    }
}