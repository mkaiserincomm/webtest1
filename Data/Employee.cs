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
        public string title {get; set; }
        
        [Display(Name ="Title of Courtesy")] 
        public string titleOfCourtesy {get; set; }
        
        [Display(Name ="Birth Date")] 
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime birthDate {get; set; }
        
        [Display(Name ="Hire Date")] 
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime hireDate {get; set; }
        public string address {get; set; }
        public string city {get; set; }
        public string region {get; set; }
        public string postalCode {get; set; }
        public string country {get; set; }
        public string homePhone {get; set; }
        public string extension {get; set; }
        public Byte[] photo {get; set; }
        public string notes {get; set; }
        public Int32? reportsTo {get; set; }        
        public string photoPath {get; set; }
    }
}