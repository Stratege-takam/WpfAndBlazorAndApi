using Brewery.BL.Contracts.Requests.Orders;
using Brewery.BL.Contracts.Responses.Orders;
using Elia.Core.Enums;
using Elia.Core.Utils;
using Newtonsoft.Json;
using Xunit;

namespace Brewery.API.Spec.Steps.OrderBeers;

[Binding]
public class OrderBeerStep5Definitions
{
    private CreateOrderRequest Request { get; set; }
    private BaseHttpResponse<EstimateOrderResponse> Response { get; set; }
    
    [Given(@"The user fills in the requested information with not exist stock beer \((.*), (.*), (.*)\)")]
    public void GivenTheUserFillsInTheRequestedInformationWithNotExistStockBeer(Guid clientId, Guid wholesalerId, string beers)
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

    [When(@"He submit the form wholesaler with not exist stock beer")]
    public async Task WhenHeSubmitTheFormWholesalerWithNotExistStockBeer()
    {
        
        var response  = await Hooks.Hooks.ServerRestService.RunAsync<BaseHttpResponse<EstimateOrderResponse>>($"{Hooks.Hooks.BaseUrl}/OrderBeer",
            Verb.POST, Request);
        if (response.IsSuccess)
        {
            Response = response.Data;
        }
    }

    [Then(@"The server return to status code BadParams because the beer does't exist stock")]
    public void ThenTheServerReturnToStatusCodeBadParamsBecauseTheBeerDoestExistStock()
    {
        Assert.Equal(BaseResultStatus.BadParams, Response.ResultStatus);
    }

    [Then(@"The reason is The number of beers \(xxx\) ordered must not exceed the wholesaler's stock")]
    public void ThenTheReasonIsTheNumberOfBeersXxxOrderedMustNotExceedTheWholesalersStock()
    {
        Assert.StartsWith("The number of beers (", Response.Reason);
        Assert.EndsWith(") ordered must not exceed the wholesaler's stock", Response.Reason);
    }
}