using System.ComponentModel.DataAnnotations;

namespace Medicalstore.Models
{
    public class Medicine_Master
    {
  
        public int med_id { get; set; }
        [Required(ErrorMessage = "Please Enter your medicine name")]
        [RegularExpression(@"^[0-9a-zA-Z ]+$", ErrorMessage = "Please enter only character for medicine medicine name")]

        public string med_nm{ get; set; }
        [Required]
        public int cat_id { get; set; }
        [Required]
        public int brand_id { get; set; }
        [Required(ErrorMessage = "Please Enter medicine-content")]
        [RegularExpression(@"^[0-9a-zA-Z ]+$", ErrorMessage = "Please enter only character for medicine medicine name")]
        public string contents{ get;set; }
        [Required]
        public int rate { get; set; }
        [Required]
        public int stock { get;set; }

    }
}
