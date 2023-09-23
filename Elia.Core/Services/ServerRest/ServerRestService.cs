using System.Net.Http.Headers;
using System.Net.Http.Json;
using Elia.Core.Attributes;
using Elia.Core.Enums;
using Elia.Core.Utils;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;


namespace Elia.Core.Services.ServerRest;


    /// <summary>
    /// 
    /// </summary>
    [Injectable(serviceLifetime: ServiceLifetime.Singleton)]
    public class  ServerRestService 
    {
        /// <summary>
        /// 
        /// </summary>
        public  int? Port = null;
        
        /// <summary>
        /// 
        /// </summary>
        public string Host { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public const string Type = "application/json";
        
        /// <summary>
        /// 
        /// </summary>
        public string Route { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }
        
    
        /// <summary>
        /// 
        /// </summary>
        public string Url  {
            get
            {
                var str = Host;

                str += Port != null ? ":" + Port : "";
                str += Route != null ? "/" + Route : "";

                return str;
            }
        }
            
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="httpVerb"></param>
        /// <param name="ob"></param>
        /// <param name="type"></param>
        /// <param name="authaurization"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> GetResponseAsync(string host, Verb httpVerb, object ob = null,
            string type = Type, string token = null, string authaurization = "Bearer")
        {
            this.Host = host ?? this.Host;
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(type?? Type));
            
            if (!string.IsNullOrEmpty(token ?? Token))
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(authaurization,
                    token ?? Token);
            
            HttpResponseMessage result = new HttpResponseMessage();
          switch (httpVerb)
            {
                case Verb.POST:
                    result = await client.PostAsJsonAsync(this.Url, ob);
                    //  result = client.PostAsync(this.Url, GetByteArrayContent(ob, type)).Result;
                    break;

                case Verb.PUT:
                    result = await client.PutAsJsonAsync(this.Url, ob);
                    // result = client.PutAsync(this.Url, GetByteArrayContent(ob, type)).Result;
                    break;

                case Verb.DELETE:
                    result = await client.DeleteAsync(this.Url);
                    break;

                case Verb.GET:

                    result = await client.GetAsync(this.Url);
                    break;
            }
            return result;

        }


        /// <summary>
        /// Get result from server 
        /// </summary>
        /// <param name="host"> reprent the server url </param>
        /// <param name="httpVerb">respresent the verb like Verb.POST, Verb.GET</param>
        /// <param name="ob">represent the object that you want to sent</param>
        /// <param name="type">respresent type of result like application/json </param>
        /// <param name="token"></param>
        /// <param name="authaurization">represent the schema of request like Bearer</param>
        /// <returns></returns>
        public async Task<BaseResult<T>> RunAsync<T>(string host, Verb httpVerb, object ob = null, string type = null,
             string token = null, string authaurization = "Bearer")
            where T: class
        {

            try
            {
                var response = await GetResponseAsync(host, httpVerb, ob, type, token, authaurization);
                
                var result = await response.Content.ReadAsStringAsync();

                return new BaseResult<T>(JsonConvert.DeserializeObject<T>(result));
            }
            catch (Exception e)
            {

                return new BaseResult<T>(e);
            }
            
        }


    }
