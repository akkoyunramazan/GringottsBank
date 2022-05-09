namespace Transaction.Api.Models
{
    public class TransactionModel
    {
        public int AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}