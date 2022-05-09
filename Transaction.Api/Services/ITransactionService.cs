using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transaction.Api.Models;

namespace Transaction.Api.Services
{
    public interface ITransactionService
    {
        Task<TransactionResult> Deposit(AccountTransaction accountTransaction);
        Task<TransactionResult> Withdraw(AccountTransaction accountTransaction);

        Task<List<TransactionInquiryResult>> GetTransactionsByAccountNumber(int accountNumber);
        
        Task<List<TransactionInquiryResult>> GetTransactionsByCustomerNumber(int customerNumber, DateTime startDate, DateTime endDate);
    }
}