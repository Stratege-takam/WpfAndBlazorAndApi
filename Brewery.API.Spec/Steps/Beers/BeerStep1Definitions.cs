using Brewery.BL.Contracts.Requests.Beers;
using Brewery.BL.Contracts.Responses.Beers;
using Elia.Core.Enums;
using Elia.Core.Utils;
using Xunit;

namespace Brewery.API.Spec.Steps.Beers;

[Binding]
[CollectionDefinition("SpecFlowNonParallelizableFeatures", DisableParallelization = true)]
public class BeerStep1Definitions
{

    private CreateBeerRequest Request { get; set; }
    private BaseHttpResponse<CreateBeerResponse> Response { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    private readonly ScenarioContext _scenarioContext;


    public BeerStep1Definitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        Request  = new CreateBeerRequest();
    }


    [Given(@"User fills in the name of the beer ""(.*)""")]
    public void GivenUserFillsInTheNameOfTheBeer(string name)
    {
        Request.Name = name;
    }

    [Given(@"User enters the identifier of the beer brewer ""(.*)""")]
    public void GivenUserEntersTheIdentifierOfTheBeerBrewer(string ownerId)
    {
        Request.OwnerId = Guid.Parse(ownerId);
    }

    [Given(@"User enters the price of the beer (.*)")]
    public void GivenUserEntersThePriceOfTheBeer(double price)
    {
        Request.Price = price;
    }

    [Given(@"User enters the degree of the beer (.*)")]
    public void GivenUserEntersTheDegreeOfTheBeer(double degree)
    {
        Request.Degree = degree;
    }

    [When(@"User submit result")]
    public async Task WhenUserSubmitResult()
    {
        var response  = await Hooks.Hooks.ServerRestService.RunAsync<BaseHttpResponse<CreateBeerResponse>>($"{Hooks.Hooks.BaseUrl}/Beer",
            Verb.POST, Request);

        if (response.IsSuccess)
        {
            Response = response.Data;
        }
    }
   
    [Then(@"The status code is Success")]
    public void ThenTheStatusCodeIsSuccess()
    {
        Assert.Equal(Response.ResultStatus, Response.ResultStatus);
    }

    [Then(@"The degree must be ""(.*)""")]
    public void ThenTheDegreeMustBe(string degree)
    {
        Assert.Equal(Response.Data.Degree, degree);
    }

    [Then(@"The identifiant must be not null")]
    public void ThenTheIdentifiantMustBeNotNull()
    {
        Assert.NotNull(Response.Data.Id);
    }
}