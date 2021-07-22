using System;
using System.ComponentModel.DataAnnotations;

namespace webtest1.Data
{    
    public class Employee
    {

        /*
        	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
            [LastName] [nvarchar](20) NOT NULL,
            [FirstName] [nvarchar](10) NOT NULL,
            [Title] [nvarchar](30) NULL,
            [TitleOfCourtesy] [nvarchar](25) NULL,
            [BirthDate] [datetime] NULL,
            [HireDate] [datetime] NULL,
            [Address] [nvarchar](60) NULL,
            [City] [nvarchar](15) NULL,
            [Region] [nvarchar](15) NULL,
            [PostalCode] [nvarchar](10) NULL,
            [Country] [nvarchar](15) NULL,
            [HomePhone] [nvarchar](24) NULL,
            [Extension] [nvarchar](4) NULL,
            [Photo] [image] NULL,
            [Notes] [ntext] NULL,
            [ReportsTo] [int] NULL,
            [PhotoPath] [nvarchar](255) NULL,
        */
        
        [Display(Name ="Employee Id")]        
        public int employeeId {get; set; }
        
        [Display(Name ="Last Name")]  
        [MaxLength(20)]      
        public string lastName {get; set; }
        
        [Display(Name ="First Name")]        
        [MaxLength(10)]
        public string firstName {get; set; }
        
        [Display(Name ="Title")] 
        [MaxLength(30)]               
        public string title {get; set; }
        
        [Display(Name ="Title of Courtesy")] 
        [MaxLength(25)]
        public string titleOfCourtesy {get; set; }
        
        [Display(Name ="Birth Date")] 
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime birthDate {get; set; }
        
        [Display(Name ="Hire Date")] 
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime hireDate {get; set; }
        
        [Display(Name ="Address")] 
        [MaxLength(60)]
        public string address {get; set; }
        
        [Display(Name ="City")] 
        [MaxLength(25)]
        public string city {get; set; }
        
        [Display(Name ="Region")] 
        [MaxLength(25)]
        public string region {get; set; }
        
        [Display(Name ="Postal Code")] 
        [MaxLength(10)]
        [DataType(DataType.PostalCode)]
        public string postalCode {get; set; }
        
        [Display(Name ="Country")] 
        [MaxLength(15)]
        public string country {get; set; }
        
        [Display(Name ="Home Phone")] 
        [MaxLength(24)]
        [DataType(DataType.PhoneNumber)]
        public string homePhone {get; set; }
        
        [Display(Name ="Extension")] 
        [MaxLength(4)]
        public string extension {get; set; }
        
        [Display(Name ="Photo")]         
        public Byte[] photo {get; set; }
        
        [Display(Name ="Notes")]  
        public string notes {get; set; }
        
        [Display(Name ="Reports To")]         
        public Int32? reportsTo {get; set; }        
        
        [Display(Name ="Photo Path")] 
        [MaxLength(255)]
        public string photoPath {get; set; }
    }
}