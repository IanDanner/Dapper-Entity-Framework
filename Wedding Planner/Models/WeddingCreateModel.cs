using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Wedding_Planner.Models
{
    public class WeddingCreate
    {   
        [Required]
        [MinLength(2)]
        [Display(Name = "Bride's Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Bride's name can only contain letters")]
        public string bride { get; set; }
        [Required]
        [MinLength(2)]
        [Display(Name = "Groom's Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Groom's name can only contain letters")]
        public string groom { get; set; }
        [Required]
        [Display(Name = "Date of Wedding")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        [DataType(DataType.Date)]
        public DateTime date_of_wedding { get; set; }
        [Required]
        [Display(Name = "Wedding Location")]
        public string weddingLocation { get; set; }
    }
}