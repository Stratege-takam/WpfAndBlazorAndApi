using Brewery.BL.Contracts.Requests.Orders;
using Brewery.BL.Contracts.Responses.Orders;
using Elia.Core.Enums;
using Elia.Core.Utils;
using Newtonsoft.Json;
using Xunit;

namespace Brewery.API.Spec.Steps.OrderBeers;

[Binding]
public class OrderBeerStep7Definitions
{
    private CreateOrderRequest Request { get; set; }
    private BaseHttpResponse<EstimateOrderResponse> Response { get; set; }
    
    [Given(@"The user fills in the requested information with non exist client \((.*), (.*), (.*)\)")]
    public void GivenTheUserFillsInTheRequestedInformationWithNonExistClient(Guid clientId, Guid wholesalerId, string beers)
    {
        var json = beers.Replace(";", ",");
        var items = JsonConvert.DeserializeObject<List<EstimationOrderItemRequest>>(json);
        Request = new CreateOrderRequest()
        {
            WholesalerId = wholesalerId,
            ClientId = clientId,
            Beers = items
        };
    }


    [When(@"He submit the form wholesaler with non exist client")]
    public async Task WhenHeSubmitTheFormWholesalerWithNonExistClient()
    {
        var response  =await Hooks.Hooks.ServerRestService.RunAsync<BaseHttpResponse<EstimateOrderResponse>>($"{Hooks.Hooks.BaseUrl}/OrderBeer",
            Verb.POST, Request);
        if (response.IsSuccess)
        {
            Response = response.Data;
        }
    }

    [Then(@"The server return to status code BadParams becuase non exist client")]
    public void ThenTheServerReturnToStatusCodeBadParamsBecuaseNonExistClient()
    {
        Assert.Equal(BaseResultStatus.BadParams, Response.ResultStatus);
    }

    [Then(@"The reason is The client must exist")]
    public void ThenTheReasonIsTheClientMustExist()
    {
        Assert.Equal("The client must exist", Response.Reason);
    }
}