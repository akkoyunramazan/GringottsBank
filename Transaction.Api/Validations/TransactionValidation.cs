using System.Threading.Tasks;
using Transaction.Api.Exceptions;
using Transaction.Api.Models;
using Transaction.Api.Types;

namespace Transaction.Api.Validations
{
    public static class TransactionValidation
    {
        public static async Task Validate(this AccountTransaction accountTransaction, AccountSummary accountSummary)
        {
            var amount = accountTransaction.Amount;

            if (amount.Currency == Currency.Unknown)
            {
                throw new InvalidCurrencyException(amount.Currency.ToString());
            }

            if (amount <= 0)
            {
                throw new InvalidAmountException(amount.Amount);
            }

            if (accountTransaction.TransactionType == TransactionType.Withdrawal)
            {
                if (amount > accountSummary.Balance)
                {
                    throw new InsufficientBalanceException();
                }
            }

            await Task.CompletedTask;
        }

        public static async Task Validate(this AccountSummary accountSummary, int accountNumber)
        {
            if (accountSummary == null)
            {
                throw new InvalidAccountNumberException(accountNumber);
            }            

            await Task.CompletedTask;
        }
    }
}