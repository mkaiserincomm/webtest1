using System.ComponentModel.DataAnnotations;

namespace webtest1.Data
{    
    public class Customer 
    {
        [Display(Name ="Customer Id")]        
        [MaxLength(5)]        
        public string customerId {get; set;}

        [Display(Name ="Company Name")]
        [Required]
        [MaxLength(40)]
        public string companyName {get; set;}        

        [Display(Name ="Contact Name")]
        [Required]
        [MaxLength(30)]
        public string contactName {get; set;}

        [Display(Name ="Contact Title")]
        [Required]
        [MaxLength(30)]
        public string contactTitle {get; set;}

        [Display(Name ="Address")]
        [Required]
        [MaxLength(60)]
        public string address {get; set;}

        [Display(Name ="City")]
        [Required]
        [MaxLength(60)]
        public string city {get; set;}

        [Display(Name ="Region")]
        [Required]
        [MaxLength(15)]
        public string region {get; set;}

        [Display(Name ="Postal Code")]
        [Required]
        [MaxLength(15)]
        [DataType(DataType.PostalCode)]
        public string postalCode {get; set;}

        [Display(Name ="Country")]
        [Required]        
        [MaxLength(15)]
        public string country {get; set;}

        [Display(Name ="Phone")]        
        [MaxLength(24)]
        [DataType(DataType.PhoneNumber)]
        public string phone {get; set;}

        [Display(Name ="Fax")]
        [MaxLength(24)]
        [DataType(DataType.PhoneNumber)]
        public string fax {get; set;}
    }
}