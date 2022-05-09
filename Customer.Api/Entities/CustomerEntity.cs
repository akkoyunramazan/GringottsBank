using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.Api.Entities
{
    [Table("CustomerInformation")]
    public class CustomerEntity
    {
        [Key]
        public int CustomerNumber { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}