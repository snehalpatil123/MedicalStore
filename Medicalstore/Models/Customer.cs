using System.ComponentModel.DataAnnotations;

namespace Medicalstore.Models
{
    public class Customer
    {
      
        public int cust_id { get; set; }
        [Required(ErrorMessage = "Please Enter your customer name")]
        [RegularExpression(@"^[0-9a-zA-Z ]+$", ErrorMessage = "Please enter only character for customer name")]
        public string cust_nm { get; set; }
        [Required(ErrorMessage = "Please Enter customer address")]
        [RegularExpression(@"^[0-9a-zA-Z ]+$", ErrorMessage = "plese enter character and numbers for  address ")]
        public string cust_address { get;set; }
        [Required(ErrorMessage = "Please Enter customer Mobile number")]
        [RegularExpression(@"^(?:\+?91[-. ]?)?\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "plese enter only number  ")]
        public string cust_phone { get; set; }
        [Required(ErrorMessage = "Please Enter customer Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "plese enter only character ,number and symbolls ")]
        public string cust_email { get;set;}
    }
}
