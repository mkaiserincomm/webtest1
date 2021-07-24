using System.Collections.Generic;
using webtest1.Data;
using webtest1.Models;

namespace webtest1.Tests.TestData
{
    // Test Data
    public class GetCategoryTestData
    {
        public static List<Category> TestData = new List<Category>
        {
            new Category{
                categoryId = 1,
                categoryName = "First",
                description = "description of first",
                picture = new byte[] { 1, 2, 3, 4 }
            },
            new Category{
                categoryId = 2,
                categoryName = "Second",
                description = "description of second",
                picture = new byte[] { 5, 6, 7, 8 }
            }
        };    
    }

    // Test cases for CategoryController_GetCategory_Success
    public class GetCategoryTestCases : IEnumerable<object[]>
    {                            
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 
                new DataViewModel<Category>{
                    Data = null,
                    Current = new Category {
                        categoryId = 1,                        
                        description = "descriptionOfFirst",
                        picture = new byte[] {1, 2, 3, 4}
                    },
                    Action = "updatedata",
                    Id = "1"
                }, 
                "List", true, true, "updatedata, model is valid, put succeeds"
            };

            yield return new object[] { 
                new DataViewModel<Category>{
                    Data = null,
                    Current = new Category {
                        categoryId = 1,                        
                        description = "descriptionOfFirst",
                        picture = new byte[] {1, 2, 3, 4}
                    },
                    Action = "updatedata",
                    Id = "1"
                }, 
                "Edit", true, false, "updatedata, model is valid, put fails"
            };

            yield return new object[] { 
                new DataViewModel<Category>{
                    Data = null,
                    Current = new Category {
                        categoryId = 1,
                        categoryName = "First",
                        description = "descriptionOfFirst",
                        picture = new byte[] {1, 2, 3, 4}
                    },
                    Action = "updatedata",
                    Id = "1"
                }, 
                "Edit", false, true, "updatedata, model is not valid" 
            };

            yield return new object[] { 
                new DataViewModel<Category>{
                    Data = null,
                    Current = new Category {
                        categoryId = 1,                        
                        description = "descriptionOfFirst",
                        picture = new byte[] {1, 2, 3, 4}
                    },
                    Action = "insertdata",
                    Id = "1"
                }, 
                "List", true, true, "updatedata, model is valid, put succeeds"
            };

            yield return new object[] { 
                new DataViewModel<Category>{
                    Data = null,
                    Current = new Category {
                        categoryId = 1,                        
                        description = "descriptionOfFirst",
                        picture = new byte[] {1, 2, 3, 4}
                    },
                    Action = "insertdata",
                    Id = "1"
                }, 
                "Insert", true, false, "updatedata, model is valid, put fails"
            };

            yield return new object[] { 
                new DataViewModel<Category>{
                    Data = null,
                    Current = new Category {
                        categoryId = 1,
                        categoryName = "First",
                        description = "descriptionOfFirst",
                        picture = new byte[] {1, 2, 3, 4}
                    },
                    Action = "insertdata",
                    Id = "1"
                }, 
                "Insert", false, true, "updatedata, model is not valid" 
            };

            yield return new object[] { 
                new DataViewModel<Category>{
                    Data = null,
                    Current = null,
                    Action = "edit",
                    Id = "1"
                }, 
                "Edit", true, true, "edit, model is valid, get succeeds" 
            };

            yield return new object[] { 
                new DataViewModel<Category>{
                    Data = null,
                    Current = null,
                    Action = "insert"
                }, 
                "Insert", true, true, "insert" 
            };

            yield return new object[] { 
                new DataViewModel<Category>{
                    Data = null,
                    Current = null,
                    Action = "delete",
                    Id = "1"
                }, 
                "List", true, true, "delete, model is valid" 
            };

            yield return new object[] { 
                new DataViewModel<Category>{
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