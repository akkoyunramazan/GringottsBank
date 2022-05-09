using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Api.Entities
{
    [Table("CustomerInformation")]
    public class UserEntity
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