using System.ComponentModel.DataAnnotations;

namespace Medicalstore.Models
{
    public class Sale_master
    {
   
        public int sale_id { get; set; }   
   
        public DateTime sale_date { get; set; } 
        [Required]
        public int cust_id { get; set; }    
        [Required]
        public double grand_total { get; set; } 
    }
}
