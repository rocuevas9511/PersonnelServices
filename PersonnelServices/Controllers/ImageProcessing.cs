using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PersonnelServices.Controllers
{    
    public static class ImageProcessing
    {
        const string subscriptionKey = "1b571353cbdf4e97b9e341e88c118d55";
        const string uriBase = "https://centralus.api.cognitive.microsoft.com/face/v1.0/";
        const string requestParameters = "returnFaceId=true&returnFaceLandmarks=false" +
                "&returnFaceAttributes=emotion";

        public static async Task<string> MakeAnalysisRequest(byte[] byteData)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add(
                "Ocp-Apim-Subscription-Key", subscriptionKey);
            
            string uri = uriBase + "detect?" + requestParameters;
            
            HttpResponseMessage response;

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {                
                content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");
                
                response = await client.PostAsync(uri, content);                
                string contentString = await response.Content.ReadAsStringAsync();
                return contentString;
            }
        }
    }
}
