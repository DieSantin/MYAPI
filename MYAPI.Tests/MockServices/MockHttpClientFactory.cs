using Moq.Protected;
using Moq;
using System.Net;

namespace MYAPI.Tests.MockServices;

public class MockHttpClientFactory
{
    public HttpClient Client { get; private set; }

    public MockHttpClientFactory(HttpStatusCode httpStatusCode)
    {
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = httpStatusCode,
                Content = new StringContent(FakeEXAPIContentString.EXAPIFakeString)
            });
        Client = new HttpClient(mockHttpMessageHandler.Object);
    }
}



