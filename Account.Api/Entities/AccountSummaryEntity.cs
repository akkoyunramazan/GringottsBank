using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Account.Api.Entities
{
    [Table("AccountSummary", Schema = "dbo")]
    public class AccountSummaryEntity
    {
        [Key]
        public int AccountNumber { get; set; }
        public int CustomerNumber { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
    }
}