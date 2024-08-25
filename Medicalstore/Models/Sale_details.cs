using System.ComponentModel.DataAnnotations;

namespace Medicalstore.Models
{
    public class Sale_details
    {
   
        public int sale_det_id { get; set; }
        [Required]
        public int sale_id { get; set; }
        [Required]
        public int med_id { get; set; }
        [Required(ErrorMessage = "Please Enter your rate")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please Enter only number for rate")]
        public int rate { get; set; }
        [Required(ErrorMessage = "Please Enter your rate")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please Enter only number for rate")]
        public int qty { get; set; }
        [Required(ErrorMessage = "Please Enter the quality")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please Enter only number for quality")]
        public int amt { get; set; }
        [Required(ErrorMessage = "Please Enter gst")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please Enter only number for gst")]
        public double gst { get; set; }
        [Required]
        public double total { get; set; }

    }
}
