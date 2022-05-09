using Transaction.Api.Types;

namespace Transaction.Api.Models
{
    public class AccountSummary
    {
        public int AccountNumber { get; set; }
        
        public int CustomerNumber { get; set; }
        public Money Balance { get; set; }
        
    }
}