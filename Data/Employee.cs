using System;
using System.ComponentModel.DataAnnotations;

namespace webtest1.Data
{    
    public class Employee
    {
        
        [Display(Name ="Employee Id")]        
        public int employeeId {get; set; }
        
        [Display(Name ="Last Name")]        
        public string lastName {get; set; }
        
        [Display(Name ="First Name")]        
        public string firstName {get; set; }
        
        [Display(Name ="Title")]        
        public string Title {get; set; }
        public string TitleOfCourtesy {get; set; }
        public DateTime BirthDate {get; set; }
        public DateTime HireDate {get; set; }
        public string Address {get; set; }
        public string City {get; set; }
        public string Region {get; set; }
        public string PostalCode {get; set; }
        public string Country {get; set; }
        public string HomePhone {get; set; }
        public string Extension {get; set; }
        public Byte[] Photo {get; set; }
        public string Notes {get; set; }
        public Int32? ReportsTo {get; set; }        
        public string PhotoPath {get; set; }
    }
}