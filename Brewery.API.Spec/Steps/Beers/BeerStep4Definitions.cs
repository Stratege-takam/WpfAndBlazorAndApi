using Brewery.BL.Contracts.Requests.Beers;
using Brewery.BL.Contracts.Responses.Beers;
using Elia.Core.Enums;
using Elia.Core.Utils;
using Xunit;

namespace Brewery.API.Spec.Steps.Beers;

[Binding]
public class BeerStep4Definitions
{
    
    private CreateBeerRequest Request { get; set; }
    private BaseHttpResponse<CreateBeerResponse> Response { get; set; }
    
    [Given(@"The user fills the form \((.*),(.*),(.*),(.*)\)")]
    public void GivenTheUserFillsTheForm(string name, Guid ownerId, double price, double degree)
    {
        Request = new CreateBeerRequest()
        {
            Degree = degree,
            Name = name,
            Price = price,
            OwnerId = ownerId
        };
    }

    [When(@"He submit")]
    public async Task WhenHeSubmit()
    {
        var response  = await Hooks.Hooks.ServerRestService.RunAsync<BaseHttpResponse<CreateBeerResponse>>($"{Hooks.Hooks.BaseUrl}/Beer",
            Verb.POST, Request);
        if (response.IsSuccess)
        {
            Response = response.Data;
        }
    }
    
    [Then(@"Every status code is BadParams")]
    public void ThenEveryStatusCodeIsBadParams()
    {
        Assert.Equal(BaseResultStatus.BadParams, Response.ResultStatus);
    }

    [Then(@"The reason is A beer already exists under this name\. Beer name must be unique")]
    public void ThenTheReasonIsABeerAlreadyExistsUnderThisNameBeerNameMustBeUnique()
    {
        Assert.Equal("A beer already exists under this name. Beer name must be unique", Response.Reason);
    }

    [Then(@"The data is null because an existing beer")]
    public void ThenTheDataIsNullBecauseAnExistingBeer()
    {
        Assert.Null(Response.Data);
    }
}