using System;
using System.ComponentModel.DataAnnotations;

namespace RESTauranter.Models
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
        [DataType(DataType.Date)]     
        public DateTime created_at { get; set; }

        [DataType(DataType.Date)]      
        public DateTime updated_at { get; set; }
    }
    public class Review : BaseEntity
    {
        [Key]
        public long id { get; set;}

        [Required]
        [MinLength(2)]
        [Display(Name = "Reviewer Name")]
        public string reviewer { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Restaurant Name")]
        public string restaurant { get; set; }

        [Required]
        [MinLength(2)]        
        [Display(Name = "Review")]
        public string review { get; set; }

        [Required]
        [Display(Name = "Stars")]
        public int stars { get; set; }

        [Required]
        [Display(Name = "Date of Visit")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        [DataType(DataType.Date)]
        public DateTime date_of_visit { get; set; }
    }
}