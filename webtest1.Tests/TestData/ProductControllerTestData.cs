using System.Collections.Generic;
using webtest1.Data;
using webtest1.Models;

namespace webtest1.Tests.TestData
{
    // Test Data
    public class GetProductTestData
    {
        public static List<Product> TestData = new List<Product>
        {
            new Product{                
                productId = 1,
                
            },
            new Product{
                productId = 2,
                
            }
        };    
    }

    // Test cases for ProductController_GetProduct_Success
    public class GetProductTestCases : IEnumerable<object[]>
    {                            
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 
                new DataViewModel<Product>{
                    Data = null,
                    Current = GetProductTestData.TestData[0],                    
                    Action = "updatedata",
                    Id = "1"
                }, 
                "List", true, true, "updatedata, model is valid, put succeeds"
            };

            yield return new object[] { 
                new DataViewModel<Product>{
                    Data = null,
                    Current = GetProductTestData.TestData[0],
                    Action = "updatedata",
                    Id = "1"
                }, 
                "Edit", true, false, "updatedata, model is valid, put fails"
            };

            yield return new object[] { 
                new DataViewModel<Product>{
                    Data = null,
                    Current = GetProductTestData.TestData[0],
                    Action = "updatedata",
                    Id = "1"
                }, 
                "Edit", false, true, "updatedata, model is not valid" 
            };

            yield return new object[] { 
                new DataViewModel<Product>{
                    Data = null,
                    Current = GetProductTestData.TestData[0],
                    Action = "insertdata",
                    Id = "1"
                }, 
                "List", true, true, "updatedata, model is valid, put succeeds"
            };

            yield return new object[] { 
                new DataViewModel<Product>{
                    Data = null,
                    Current = GetProductTestData.TestData[0],
                    Action = "insertdata",
                    Id = "1"
                }, 
                "Insert", true, false, "updatedata, model is valid, put fails"
            };

            yield return new object[] { 
                new DataViewModel<Product>{
                    Data = null,
                    Current = GetProductTestData.TestData[0],
                    Action = "insertdata",
                    Id = "1"
                }, 
                "Insert", false, true, "updatedata, model is not valid" 
            };

            yield return new object[] { 
                new DataViewModel<Product>{
                    Data = null,
                    Current = null,
                    Action = "edit",
                    Id = "1"
                }, 
                "Edit", true, true, "edit, model is valid, get succeeds" 
            };

            yield return new object[] { 
                new DataViewModel<Product>{
                    Data = null,
                    Current = null,
                    Action = "edit",
                    Id = "1"
                }, 
                "Edit", true, false, "edit, model is valid, get fails" 
            };

            yield return new object[] { 
                new DataViewModel<Product>{
                    Data = null,
                    Current = null,
                    Action = "insert"
                }, 
                "Insert", true, true, "insert" 
            };

            yield return new object[] { 
                new DataViewModel<Product>{
                    Data = null,
                    Current = null,
                    Action = "delete",
                    Id = "1"
                }, 
                "List", true, true, "delete, model is valid" 
            };

            yield return new object[] { 
                new DataViewModel<Product>{
                    Data = null,
                    Current = null,
                    Action = ""                    
                }, 
                "List", true, true, "other" 
            };
            
        }
        
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}