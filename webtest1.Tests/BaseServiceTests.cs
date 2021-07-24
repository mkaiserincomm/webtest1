using Moq;
using Moq.Protected;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace webtest1.Tests
{
    public class BaseServiceTest
    {
        protected Mock<HttpMessageHandler> httpMessageHandler;
        protected Mock<IHttpClientFactory> httpClientFactory;
        protected HttpRequestMessage httpRequestMessage;
                 
        protected void SetupHttpClientFactory(System.Net.HttpStatusCode statusCode, string resultString)
        {
            httpMessageHandler = new Mock<HttpMessageHandler>();
            httpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(),                    
                    ItExpr.IsAny<CancellationToken>()                    
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = statusCode,
                    Content = new StringContent(resultString),                    
                })                
                .Verifiable("Message Handler was not Called"); 
            
            httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory
                .Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient(httpMessageHandler.Object))                   
                .Verifiable("Client Not Created");            
        }       

        protected void SetupHttpClientFactory(System.Net.HttpStatusCode statusCode)
        {
            httpMessageHandler = new Mock<HttpMessageHandler>();
            httpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(),                    
                    ItExpr.IsAny<CancellationToken>()                    
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = statusCode
                })                
                .Verifiable("Message Handler was not Called"); 
            
            httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory
                .Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient(httpMessageHandler.Object))                
                .Verifiable("Client Not Created");            
        }       
    }        
}
