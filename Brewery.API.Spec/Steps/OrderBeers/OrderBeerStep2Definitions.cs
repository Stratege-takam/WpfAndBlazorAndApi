using Brewery.Contract.Contracts.Requests.Orders;
using Brewery.Contract.Contracts.Responses.Orders;
using Elia.Core.Enums;
using Elia.Core.Utils;
using Xunit;

namespace Brewery.API.Spec.Steps.OrderBeers;

[Binding]
public class OrderBeerStep2Definitions
{
    private CreateOrderRequest Request { get; set; }
    private BaseHttpResponse<EstimateOrderResponse> Response { get; set; }
    
    [Given(@"The user fills incorrectly requested information \((.*), (.*)\)")]
    public void GivenTheUserFillsIncorrectlyRequestedInformation(Guid clientId, Guid wholesalerId)
    {
        Request = new CreateOrderRequest()
        {
            WholesalerId = wholesalerId,
            ClientId = clientId,
            Beers = new List<EstimationOrderItemRequest>()
        };
    }

    [When(@"He submit the form wholesaler")]
    public async Task WhenHeSubmitTheFormWholesaler()
    {
        var response  = await Hooks.Hooks.ServerRestService.RunAsync<BaseHttpResponse<EstimateOrderResponse>>($"{Hooks.Hooks.BaseUrl}/OrderBeer",
            Verb.POST, Request);
        if (response.IsSuccess)
        {
            Response = response.Data;
        }
    }

    
    [Then(@"The server answer to status code BadParams")]
    public void ThenTheServerAnswerToStatusCodeBadParams()
    {
        Assert.Equal(BaseResultStatus.BadParams, Response.ResultStatus);
    }

    [Then(@"The reason is Order cannot be empty")]
    public void ThenTheReasonIsOrderCannotBeEmpty()
    {
        Assert.Equal("Order cannot be empty", Response.Reason);
    }
}