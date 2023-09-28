using Brewery.Contract.Contracts.Requests.Orders;
using Brewery.Contract.Contracts.Responses.Orders;
using Elia.Core.Enums;
using Elia.Core.Utils;
using Newtonsoft.Json;
using Xunit;

namespace Brewery.API.Spec.Steps.OrderBeers;

[Binding]
public class OrderBeerStep4Definitions
{
    private CreateOrderRequest Request { get; set; }
    private BaseHttpResponse<EstimateOrderResponse> Response { get; set; }
    
    [Given(@"The user fills in the requested information with not exist beer \((.*), (.*), (.*)\)")]
    public void GivenTheUserFillsInTheRequestedInformationWithNotExistBeer(Guid clientId, Guid wholesalerId, string beers)
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

    [When(@"He submit the form wholesaler with not exist beer")]
    public async Task WhenHeSubmitTheFormWholesalerWithNotExistBeer()
    {
        var response  = await Hooks.Hooks.ServerRestService.RunAsync<BaseHttpResponse<EstimateOrderResponse>>($"{Hooks.Hooks.BaseUrl}/OrderBeer",
            Verb.POST, Request);
        if (response.IsSuccess)
        {
            Response = response.Data;
        }
    }

    [Then(@"The server return to status code BadParams because the beer does't exist")]
    public void ThenTheServerReturnToStatusCodeBadParamsBecauseTheBeerDoestExist()
    {
        Assert.Equal(BaseResultStatus.BadParams, Response.ResultStatus);
    }

    [Then(@"The reason is The beer \(xxx\) must be sold by the wholesaler")]
    public void ThenTheReasonIsTheBeerXxxMustBeSoldByTheWholesaler()
    {
        Assert.StartsWith("The beer (", Response.Reason);
        Assert.EndsWith(") must be sold by the wholesaler", Response.Reason);
    }
}