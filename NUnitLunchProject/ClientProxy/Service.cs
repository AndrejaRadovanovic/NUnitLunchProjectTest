using System;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Net;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace NUnitLunchProject.ClientProxy
{
    public static class Service
    {
        public static string BaseUrl { private set; get; }

        public static void Initialize(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public static T Execute<T>(this RestRequest request) where T : new() 
        {

            var client = new RestClient(BaseUrl);
            
            request.RequestFormat = DataFormat.Json;

            RestResponse<T> response = null;
            response = client.Execute<T>(request);

            if (response == null)
            {
                throw new Exception("Invalid response");
            }
            else if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }

            throw new Exception(string.Format("Status code: {0}. Status description: {1}. Response content: {2}. Error message: {3}",
                response.StatusCode.ToString(),
                response.StatusDescription,
                response.Content,
                response.ErrorMessage
            ));
        }
        public static string Execute(this RestRequest request)
        {
            var client = new RestClient(BaseUrl);

            request.RequestFormat = DataFormat.Json;

            RestResponse response = null;
            response = client.Execute(request);
            if (response == null)
            {
                throw new Exception("Invalid response");
            }
            else if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
            {

                return JsonConvert.DeserializeObject<string>(response.Content);
                
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {

                throw new UnauthorizedAccessException();

            }
            else if (response.StatusCode == 0)
            {
                if (response.ErrorException.InnerException != null)
                {
                    throw response.ErrorException.InnerException;
                }
            }

            //For delete functions
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                //return JsonConvert.DeserializeObject<string>(response.StatusCode.ToString());
                return null;
            }


            throw new Exception(string.Format("Status code: {0}. Status description: {1}. Response content: {2}. Error message: {3}",
                 response.StatusCode.ToString(),
                 response.StatusDescription,
                 response.Content,
                 response.ErrorMessage
             ));
        }
    }
}



