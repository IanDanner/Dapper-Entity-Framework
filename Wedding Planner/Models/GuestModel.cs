using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Wedding_Planner.Models
{
    public class Guest
    {
        [Key]
        public int id { get; set; }
        public int usersId { get; set; }
        public User users { get; set; }
        public int weddingsId { get; set; }
        public Wedding weddings { get; set; }
    }
}