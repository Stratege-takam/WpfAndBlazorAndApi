using Brewery.Contract.Contracts.Requests.Orders;
using Brewery.Contract.Contracts.Responses.Orders;
using Elia.Core.Enums;
using Elia.Core.Utils;
using Newtonsoft.Json;
using Xunit;

namespace Brewery.API.Spec.Steps.OrderBeers;

[Binding]
public class OrderBeerStep3Definitions
{
    private CreateOrderRequest Request { get; set; }
    private BaseHttpResponse<EstimateOrderResponse> Response { get; set; }
    
    [Given(@"The user fills in the requested information with duplicate order \((.*), (.*), (.*)\)")]
    public void GivenTheUserFillsInTheRequestedInformationWithDuplicateOrder(Guid clientId, Guid wholesalerId, string beers)
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

    [When(@"He submit the form wholesaler with duplicate order")]
    public async Task WhenHeSubmitTheFormWholesalerWithDuplicateOrder()
    {
        
        var response  = await Hooks.Hooks.ServerRestService.RunAsync<BaseHttpResponse<EstimateOrderResponse>>($"{Hooks.Hooks.BaseUrl}/OrderBeer",
            Verb.POST, Request);
        if (response.IsSuccess)
        {
            Response = response.Data;
        }
    }

    [Then(@"The server return to status code BadParams")]
    public void ThenTheServerReturnToStatusCodeBadParams()
    {
        Assert.Equal(BaseResultStatus.BadParams, Response.ResultStatus);
    }

    [Then(@"The reason There cannot be a duplicate in the order")]
    public void ThenTheReasonThereCannotBeADuplicateInTheOrder()
    {
        Assert.Equal("There cannot be a duplicate in the order", Response.Reason);
    }
}