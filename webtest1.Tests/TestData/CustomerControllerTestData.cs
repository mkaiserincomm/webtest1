using System.Collections.Generic;
using webtest1.Data;
using webtest1.Models;

namespace webtest1.Tests.TestData
{
    // Test Data
    public class GetCustomerTestData
    {
        public static List<Customer> TestData = new List<Customer>
        {
            new Customer{                
                customerId = "1",
                companyName = "company1",
                contactName = "contact1",
                contactTitle = "title1",
                address = "address1",
                city = "city1",
                region = "11",
                postalCode = "11111",
                country = "C1",
                phone = "1111",
                fax = "1111"
            },
            new Customer{
                customerId = "2",
                companyName = "company2",
                contactName = "contact2",
                contactTitle = "title2",
                address = "address2",
                city = "city2",
                region = "22",
                postalCode = "22222",
                country = "C2",
                phone = "2222",
                fax = "2222"
            }
        };    
    }

    // Test cases for CustomerController_GetCustomer_Success
    public class GetCustomerTestCases : IEnumerable<object[]>
    {                            
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 
                new DataViewModel<Customer>{
                    Data = null,
                    Current = GetCustomerTestData.TestData[0],                    
                    Action = "updatedata",
                    Id = "1"
                }, 
                "List", true, true, "updatedata, model is valid, put succeeds"
            };

            yield return new object[] { 
                new DataViewModel<Customer>{
                    Data = null,
                    Current = GetCustomerTestData.TestData[0],
                    Action = "updatedata",
                    Id = "1"
                }, 
                "Edit", true, false, "updatedata, model is valid, put fails"
            };

            yield return new object[] { 
                new DataViewModel<Customer>{
                    Data = null,
                    Current = GetCustomerTestData.TestData[0],
                    Action = "updatedata",
                    Id = "1"
                }, 
                "Edit", false, true, "updatedata, model is not valid" 
            };

            yield return new object[] { 
                new DataViewModel<Customer>{
                    Data = null,
                    Current = GetCustomerTestData.TestData[0],
                    Action = "insertdata",
                    Id = "1"
                }, 
                "List", true, true, "updatedata, model is valid, put succeeds"
            };

            yield return new object[] { 
                new DataViewModel<Customer>{
                    Data = null,
                    Current = GetCustomerTestData.TestData[0],
                    Action = "insertdata",
                    Id = "1"
                }, 
                "Insert", true, false, "updatedata, model is valid, put fails"
            };

            yield return new object[] { 
                new DataViewModel<Customer>{
                    Data = null,
                    Current = GetCustomerTestData.TestData[0],
                    Action = "insertdata",
                    Id = "1"
                }, 
                "Insert", false, true, "updatedata, model is not valid" 
            };

            yield return new object[] { 
                new DataViewModel<Customer>{
                    Data = null,
                    Current = null,
                    Action = "edit",
                    Id = "1"
                }, 
                "Edit", true, true, "edit, model is valid, get succeeds" 
            };

            yield return new object[] { 
                new DataViewModel<Customer>{
                    Data = null,
                    Current = null,
                    Action = "insert"
                }, 
                "Insert", true, true, "insert" 
            };

            yield return new object[] { 
                new DataViewModel<Customer>{
                    Data = null,
                    Current = null,
                    Action = "delete",
                    Id = "1"
                }, 
                "List", true, true, "delete, model is valid" 
            };

            yield return new object[] { 
                new DataViewModel<Customer>{
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