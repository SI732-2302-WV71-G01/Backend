using System.Net;
using System.Net.Mime;
using System.Text;
using LearningCenter.API.Learning.Resources;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using SpecFlow.Internal.Json;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace LearningCenter.API.Test.Steps;

[Binding]
public sealed class TutorialsServiceStepDefinitions :  WebApplicationFactory<Program>
{
    private readonly WebApplicationFactory<Program> _factory;

    public TutorialsServiceStepDefinitions(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    private HttpClient Client { get; set; }
    private Uri BaseUri { get; set; }
    private Task<HttpResponseMessage> Response { get; set; }
    
    [Given(@"the Endpoint https://localhost:(.*)/api/v(.*)/tutorials is available")]
    public void GivenTheEndpointHttpsLocalhostApiVTutorialsIsAvailable(int port, int version)
    {
        BaseUri = new Uri($"https://localhost:{port}/api/v{version}/tutorials");
        Client = _factory.CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = BaseUri });
    }
    
    [When(@"a Post Request is sent")]
    public void WhenAPostRequestIsSent(Table saveTutorialResource)
    {
        var resource = saveTutorialResource.CreateSet<SaveTutorialResource>().First();
        var content = new StringContent(resource.ToJson(), Encoding.UTF8, 
            MediaTypeNames.Application.Json);
        Response = Client.PostAsync(BaseUri, content);
    }

    [Then(@"A Response is received with Status (.*)")]
    public void ThenAResponseIsReceivedWithStatus(int expectedStatus)
    {
        var expectedStatusCode = ((HttpStatusCode)expectedStatus).ToString();
        var actualStatusCode = Response.Result.StatusCode.ToString();
 
        Assert.Equal(expectedStatusCode, actualStatusCode);
    }
    
    [Then(@"a Tutorial Resource is included in Response Body")]
    public async Task ThenATutorialResourceIsIncludedInResponseBody(Table 
        expectedTutorialResource)
    {
        var expectedResource = expectedTutorialResource.CreateSet<TutorialResource>().First();
        var responseData = await Response.Result.Content.ReadAsStringAsync();
        var resource = JsonConvert.DeserializeObject<TutorialResource>(responseData);
        Assert.Equal(expectedResource.Title, resource.Title);
    }
    
    [Given(@"A Tutorial is already stored")]
    public async void GivenATutorialIsAlreadyStored(Table 
        saveTutorialResource)
    {
        var resource = saveTutorialResource.CreateSet<SaveTutorialResource>().First();
        var content = new StringContent(resource.ToJson(), Encoding.UTF8, 
            MediaTypeNames.Application.Json);
        Response = Client.PostAsync(BaseUri, content);
        var responseData = await Response.Result.Content.ReadAsStringAsync();
        var responseResource = JsonConvert.DeserializeObject<TutorialResource>(responseData);
        Assert.Equal(resource.Title, responseResource.Title);
    }
    
    [Then(@"An Error Message is returned with value ""(.*)""")]
    public void ThenAnErrorMessageIsReturnedWithValue(string expectedMessage)
    {
        var message = Response.Result.Content.ReadAsStringAsync().Result;
        Assert.Equal(expectedMessage, message);
    }
}