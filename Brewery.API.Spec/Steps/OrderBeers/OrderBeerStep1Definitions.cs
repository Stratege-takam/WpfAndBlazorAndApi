using Brewery.Contract.Contracts.Requests.Orders;
using Brewery.Contract.Contracts.Responses.Orders;
using Elia.Core.Enums;
using Elia.Core.Utils;
using Newtonsoft.Json;
using Xunit;

namespace Brewery.API.Spec.Steps.OrderBeers;

[Binding]
public class OrderBeerStep1Definitions
{
    private CreateOrderRequest Request { get; set; }
    private BaseHttpResponse<EstimateOrderResponse> Response { get; set; }
    
    [Given(@"The user fills in the requested information \((.*),(.*),(.*)\)")]
    public void GivenTheUserFillsInTheRequestedInformation(Guid clientId, Guid wholesalerId, string beers)
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
    
    [When(@"User submit the form wholesaler")]
    public async Task WhenUserSubmitTheFormWholesaler()
    {
        var response  = await Hooks.Hooks.ServerRestService.RunAsync<BaseHttpResponse<EstimateOrderResponse>>($"{Hooks.Hooks.BaseUrl}/OrderBeer",
            Verb.POST, Request);
        if (response.IsSuccess)
        {
            Response = response.Data;
        }
    }

    [Then(@"The beer is Sold with status code success")]
    public void ThenTheBeerIsSoldWithStatusCodeSuccess()
    {
        Assert.Equal(BaseResultStatus.Success, Response.ResultStatus);
    }

    [Then(@"An invoice is returned")]
    public void ThenAnInvoiceIsReturned()
    {
        Assert.NotNull(Response.Data);
    }
}