using System.ComponentModel.DataAnnotations;

namespace Medicalstore.Models
{
    public class Brand
    {
     
        public int brand_id { get; set; }
        [Required(ErrorMessage = "Please Enter your brand name")]
        [RegularExpression(@"^[0-9a-zA-Z ]+$", ErrorMessage = "Please enter only character for brand name")]
        public string brand_nm { get; set; }
    }
}
