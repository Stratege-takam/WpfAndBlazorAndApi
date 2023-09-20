using Brewery.BL.Contracts.Requests.Beers;
using Brewery.BL.Contracts.Responses.Beers;
using Elia.Core.Enums;
using Elia.Core.Utils;
using Xunit;

namespace Brewery.API.Spec.Steps.Beers;

[Binding]
public class BeerStep3Definitions
{
    
    private CreateBeerRequest Request { get; set; }
    private BaseHttpResponse<CreateBeerResponse> Response { get; set; }

    
    [Given(@"The user fills in the all requested information \((.*),(.*),(.*),(.*)\)")]
    public void GivenTheUserFillsInTheAllRequestedInformation(string name, Guid ownerId, double price, double degree)
    {
        Request = new CreateBeerRequest()
        {
            Degree = degree,
            Name = name,
            Price = price,
            OwnerId = ownerId
        };
    }

    [When(@"The user submits his request and the system processes the information")]
    public async Task WhenTheUserSubmitsHisRequestAndTheSystemProcessesTheInformation()
    {
        var response  = await Hooks.Hooks.ServerRestService.RunAsync<BaseHttpResponse<CreateBeerResponse>>($"{Hooks.Hooks.BaseUrl}/Beer",
            Verb.POST, Request);

        if (response.IsSuccess)
        {
            Response = response.Data;
        }
    }

    [Then(@"The all status code is Success")]
    public void ThenTheAllStatusCodeIsSuccess()
    {
        Assert.Equal(BaseResultStatus.Success, Response.ResultStatus);
    }

    [Then(@"The system returns each registered beer to the user with ID")]
    public void ThenTheSystemReturnsEachRegisteredBeerToTheUserWithId()
    {
        Assert.NotNull(Response.Data.Id);
    }
}