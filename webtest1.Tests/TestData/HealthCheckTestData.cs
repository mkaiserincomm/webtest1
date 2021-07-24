using System.Collections.Generic;
using webtest1.Controllers;
using webtest1.Data;
using webtest1.Models;

namespace webtest1.Tests.TestData
{
    public class HealthCheckTestData : IEnumerable<object[]>
    {                            
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new LiveCheck() };
            yield return new object[] { new StartupCheck() };
            yield return new object[] { new ReadyCheck() };    
        }
        
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}