using System.ComponentModel.DataAnnotations;

namespace webtest1.Data
{    
    public class Customer 
    {
        [Display(Name ="Customer Id")]
        public string customerId {get; set;}
        [Display(Name ="Company Name")]
        public string companyName {get; set;}        
        public string contactName {get; set;}
        public string contactTitle {get; set;}
        public string address {get; set;}
        public string city {get; set;}
        public string region {get; set;}
        public string postalCode {get; set;}
        public string country {get; set;}
        public string phone {get; set;}
        public string fax {get; set;}
    }
}