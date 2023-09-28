using Brewery.Contract.Contracts.Requests.Beers;
using Brewery.Contract.Contracts.Responses.Beers;
using Elia.Core.Enums;
using Elia.Core.Utils;
using Xunit;

namespace Brewery.API.Spec.Steps.Beers;

[Binding]
public class BeerStep2Definitions
{
    private CreateBeerRequest Request { get; set; }
    private BaseHttpResponse<CreateBeerResponse> Response { get; set; }
    
    
    [Given(@"The user fills in the requested information with a non-existent brewer \((.*),(.*),(.*),(.*)\)")]
    public void GivenTheUserFillsInTheRequestedInformationWithANonExistentBrewer(string name, Guid ownerId, double price, double degree)
    {
        Request = new CreateBeerRequest()
        {
            Degree = degree,
            Name = name,
            Price = price,
            OwnerId = ownerId
        };
    }

    [When(@"User submit the form")]
    public async Task WhenUserSubmitTheForm()
    {
        var response  = await Hooks.Hooks.ServerRestService.RunAsync<BaseHttpResponse<CreateBeerResponse>>($"{Hooks.Hooks.BaseUrl}/Beer",
            Verb.POST, Request);

        if (response.IsSuccess)
        {
            Response = response.Data;
        }
    }

    [Then(@"The status code is BadParams")]
    public void ThenTheStatusCodeIsBadParams()
    {
        Assert.Equal(BaseResultStatus.BadParams, Response.ResultStatus);
    }
    
    [Then(@"The reason is The identification of the introduced owner does not exist")]
    public void ThenTheReasonIsTheIdentificationOfTheIntroducedOwnerDoesNotExist()
    {
        Assert.Equal("The identification of the introduced owner does not exist", Response.Reason);
    }
    
    [Then(@"The data is null")]
    public void ThenTheDataIsNull()
    {
        Assert.Null(Response.Data);
    }
}