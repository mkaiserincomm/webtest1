using System.ComponentModel.DataAnnotations;

namespace webtest1.Data
{
    public class Product
    {
        [Display(Name ="Product Id")]          
        [Range(0,int.MaxValue)]
        public int productId {get; set;}
        
        [Display(Name ="Product Name")]  
        [MaxLength(40)]
        public string productName {get; set;}
        
        [Display(Name ="Supplier Id")]  
        [Range(0, int.MaxValue)]
        public int supplierId {get; set;}
        
        [Display(Name ="Category Id")]  
        [Range(0, int.MaxValue)]
        public int categoryId {get; set;}
        
        [Display(Name ="Qty Per Unit")]  
        public string quantityPerUnit {get; set;}
        
        [Display(Name ="Unit Price")]
        [DataType(DataType.Currency)]        
        public decimal unitPrice {get; set;}
        
        [Display(Name ="Units In Stock")]
        [Range(0, short.MaxValue)]          
        public System.Int16 unitsInStock {get; set;}
        
        [Display(Name ="Units On Order")]  
        [Range(0, short.MaxValue)]
        public System.Int16 unitsOnOrder {get; set;}
        
        [Display(Name ="Reorder Level")]  
        [Range(0, short.MaxValue)]
        public System.Int16 reorderLevel {get; set;}
        
        [Display(Name ="Discontinued")]          
        public bool discontinued {get; set;}      
    }
}