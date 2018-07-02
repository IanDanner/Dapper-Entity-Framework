using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Bank_Accounts.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public double balance { get; set; }
        public User()
        {
            Transactions = new List<Transaction>();
        }
        public List<Transaction> Transactions { get; set; }
    }
}