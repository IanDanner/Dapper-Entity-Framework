using System.ComponentModel.DataAnnotations;
using System;

namespace Bank_Accounts.Models
{
    public class Transaction
    {
        [Key]
        public int id { get; set; }
        public double amount { get; set; }
        public DateTime date_of_transaction { get; set; }
        public int userId { get; set; }
        public User user { get; set; }
    }
}