namespace Account.Api.Models
{
    public class AccountResultModel
    {
        public int AccountNumber { get; set; }
        public int CustomerNumber { get; set; }
        public bool IsSuccessful { get; set; }
        public decimal Balance { get; set; }
        public  string Currency { get; set; }
        public string Message { get; set; }
    }
}