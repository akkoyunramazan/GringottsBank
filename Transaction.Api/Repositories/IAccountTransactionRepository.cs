using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transaction.Api.Data.Entities;

namespace Transaction.Api.Repositories
{
    public interface IAccountTransactionRepository
    {
        Task Create(AccountTransactionEntity accountTransactionEntity, AccountSummaryEntity accountSummaryEntity);

        Task<List<AccountTransactionEntity>> GetAllTransactionsByAccount(int accountNumber);
        
        Task<List<AccountTransactionEntity>> GetAllTransactionsByCustomer(int customerNumber, DateTime startDate, DateTime endDate);

    }
}