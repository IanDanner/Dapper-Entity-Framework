using System.ComponentModel.DataAnnotations;
using System;

namespace Lost_in_the_Woods.Models
{
    public abstract class BaseEntity {}
    public class Trail : BaseEntity
    {
        [Key]
        public long Id { get; set;}

        [Required]
        [MinLength(2)]
        [Display(Name = "Trail Name")]
        public string Trail_name { get; set; }

        [Required]
        [MinLength(10)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Trail Length")]
        public int Trail_length { get; set; }

        [Required]
        [Display(Name = "Elevation Change")]
        public int Elevation_change { get; set; }

        [Required]
        [Display(Name = "Longitude")]
        [Range(-180,180)] // - = west + = east
        public float Longitude { get; set; }

        [Required]
        [Display(Name = "Latitude")]
        [Range(-90,90)] // - = south + = north
        public float Latitude { get; set; }
    }
}