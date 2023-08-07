using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UPS.EmployeesMgmt.Models;

namespace UPS.EmployeesMgmt.Services
{


   public class RestApiData
    {
        private readonly string baseApiUrl="https://gorest.co.in/public/v2/";
        public async Task<string> RestApiOperation(string apiName, string methodType, Employee emp) {


            // var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, mediaType);
            var httpRequestMessage=new HttpRequestMessage();
            var apiToken = "0bf7fb56e6a27cbcadc402fc2fce8e3aa9ac2b40d4190698eb4e8df9284e2023";
            if (methodType.ToUpper() == "GET")
            {
                 httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, baseApiUrl + apiName)
                {
                    Headers = { { HeaderNames.Authorization, $"Bearer {apiToken}" } }                    
                };
            }
            else if (methodType.ToUpper() == "PUT")
            {
                httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, baseApiUrl + apiName)
                {
                    Headers = { { HeaderNames.Authorization, $"Bearer {apiToken}" } },
                    Content= new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8,"application/json")
                };
               
            }
            else if (methodType.ToUpper() == "DELETE")
            {
                httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, baseApiUrl + apiName)
                {
                    Headers = { { HeaderNames.Authorization, $"Bearer {apiToken}" } }
                   
                };

            }
            else if (methodType.ToUpper() == "POST")
            {
                httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, baseApiUrl + apiName)
                {
                    Headers = { { HeaderNames.Authorization, $"Bearer {apiToken}" } },
                    Content = new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json")
                };

            }


            string response = string.Empty;
            using (var httpClient = new HttpClient())
            {
                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    response = await httpResponseMessage.Content.ReadAsStringAsync();
                   
                }
              
            }
            return response;
        }

    }
}
