using System.ComponentModel.DataAnnotations;

namespace Medicalstore.Models
{
    public class Medicine_Cat
    {
      
        public int cat_id { get; set; }
        [Required(ErrorMessage = "Please Enter your medicine_catgoury name")]
        [RegularExpression(@"^[0-9a-zA-Z ]+$", ErrorMessage = "Please enter only character for medicine catagoury name")]
        public string cat_nm { get; set; }
    }
}
