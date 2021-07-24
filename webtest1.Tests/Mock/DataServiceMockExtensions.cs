using System.Collections.Generic;
using Moq;
using webtest1.Services;

namespace webtest1.Tests.Mock
{
    public static class DataServiceMockExtensions
    {        
        public static Mock<IDataService<T>> GetDataServceMock<T>()
        {
            var dataService = new Mock<IDataService<T>>();                               
            return dataService;
        }

        public static Mock<IDataService<T>> AddGet<T>(this Mock<IDataService<T>> dataService, List<T> dataServiceSampleData ) 
        {            
            dataService.Setup(x => x.Get()).ReturnsAsync(dataServiceSampleData).Verifiable();            
            return dataService;
        }

        public static Mock<IDataService<T>> AddGetById<T>(this Mock<IDataService<T>> dataService, List<T> dataServiceSampleData ) 
        {
            dataService.Setup(x => x.GetById(It.Is<string>(p => p == "1"))).ReturnsAsync(dataServiceSampleData[0]);
            dataService.Setup(x => x.GetById(It.Is<string>(p => p == "2"))).ReturnsAsync(dataServiceSampleData[1]);            
            return dataService;
        }
        public static Mock<IDataService<T>> AddPut<T>(this Mock<IDataService<T>> dataService, List<T> dataServiceSampleData, bool success) 
        {            
            dataService.Setup(x => x.Put(It.IsAny<string>(), It.IsAny<T>())).ReturnsAsync(success).Verifiable();                        
            return dataService;
        }

        public static Mock<IDataService<T>> AddPost<T>(this Mock<IDataService<T>> dataService, List<T> dataServiceSampleData, bool success) 
        {            
            dataService.Setup(x => x.Post(It.IsAny<T>())).ReturnsAsync(success).Verifiable();                        
            return dataService;
        }

        public static Mock<IDataService<T>> AddDelete<T>(this Mock<IDataService<T>> dataService ) 
        {
            dataService.Setup(x => x.Delete(It.Is<string>(p => p == "1"))).ReturnsAsync(true);            
            dataService.Setup(x => x.Delete(It.Is<string>(p => p == "999"))).ReturnsAsync(false);
            return dataService;
        }
    }    
}