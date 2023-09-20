using Brewery.BL.Contracts.Requests.Mock;
using Brewery.BL.Contracts.Responses.Mock;
using Brewery.BL.Contracts.Responses.Users;
using Elia.Core.Enums;
using Elia.Core.Services.ServerRest;
using Elia.Core.Utils;

namespace Brewery.API.Spec.Hooks
{
    [Binding]
    public class Hooks
    {
        public static  ServerRestService ServerRestService { get; set; }

        public static readonly string BaseUrl = "http://127.0.0.1:5300/api";

        public static CreateUserResponse DefaultUser = null;
        private MockRequest Request { get; set; }
        
        public Hooks()
        {
            Request = new MockRequest()
            {
                Password = "DbTest2023",
                Email = "admin23@gbrewery.be"
            };
        }
        
        
       
        [Before]
        public async Task BeforeScenario()
        {
           
            ServerRestService = new ServerRestService();

            var responseClean = await ServerRestService.RunAsync<BaseHttpResponse<MockResponse>>($"{BaseUrl}/MockDb/Clean", Verb.POST,  Request);

            if (responseClean.IsSuccess)
            {
                if (responseClean.Data.ResultStatus == BaseResultStatus.Success)
                {
                    var response = await ServerRestService.RunAsync<BaseHttpResponse<MockResponse>>($"{BaseUrl}/MockDb/Seed", Verb.POST,  Request);

                    if (response.Data.ResultStatus == BaseResultStatus.Success)
                    {
                        var responseUser = await ServerRestService.RunAsync<BaseHttpResponse<CreateUserResponse>>($"{BaseUrl}/User/Login", Verb.POST,  Request);
                        if (responseUser.Data.ResultStatus == BaseResultStatus.Success)
                        {
                            DefaultUser = responseUser.Data.Data;
                            ServerRestService.Token = DefaultUser.Token;
                        }
                    }
                }
            }
            
        }

        [After]
        public  void AfterScenario()
        {
            //ManagerContext.ResetDatabases();
        }
        
    }
}