using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace webtest1.Data
{    
    public class Category
    {    
        [Display(Name ="Category Id")] 
        public int categoryId {get; set;}
        
        [Display(Name ="Category Name")]        
        [MaxLength(15)]
        [Required]
        public string categoryName {get; set;}
        
        [Display(Name ="Description")]        
        [Required]
        public string description {get; set;}
        
        public byte[] picture { get; set;} 
    }
}