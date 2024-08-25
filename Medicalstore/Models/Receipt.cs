using System.ComponentModel.DataAnnotations;

namespace Medicalstore.Models
{
    public class Receipt
    {
       
        public int rec_id { get; set; }
          
        public DateTime rec_date { get; set; }
        [Required]
        public int cust_id { get; set; }
        [Required(ErrorMessage = "Please Enter your rate")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please Enter only number for rate")]
        public int rec_amt { get; set; }    

    }
}
