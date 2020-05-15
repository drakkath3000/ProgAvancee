using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    public class Suckle
    {
        public int Id { get; set; }
        
        [Display(Name = "Suckle Date")]
        [DataType(DataType.Date)]
        public DateTime SuckleTime { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        [StringLength(30)]
        public string Side { get; set; }

    }
}

