using Transaction.Api.Types;

namespace Transaction.Api.Models
{
    public class AccountTransaction
    {
        public int AccountNumber { get; set; }
        public TransactionType TransactionType { get; set; }
        public Money Amount { get; set; }
        
        
    }
}