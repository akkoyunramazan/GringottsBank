using System;

namespace Transaction.Api.Models
{
    public class TransactionInquiryResult
    {
        public int AccountNumber { get; set; }
        public DateTime Date { get; set; }
        public string TransactionType { get; set; }  
        public decimal Amount { get; set; }
    }
}