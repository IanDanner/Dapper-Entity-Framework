using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Wedding_Planner.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public User()
        {
            CreatedWeddings = new List<Wedding>();
            Attending = new List<Guest>();
        }
        public List<Wedding> CreatedWeddings { get; set; }
        public List<Guest> Attending { get; set; }
    }
}