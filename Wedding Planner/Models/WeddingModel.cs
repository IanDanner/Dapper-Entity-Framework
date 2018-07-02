using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Wedding_Planner.Models
{
    public class Wedding
    {
        [Key]
        public int id { get; set; }
        public string bride { get; set; }
        public string groom { get; set; }
        public DateTime date_of_wedding { get; set; }
        public string weddingLocation { get; set; }
        public int userId { get; set; }
        public User user { get; set; }
        public Wedding()
        {
            Guests = new List<Guest>();
        }
        public List<Guest> Guests { get; set; }
    }
}