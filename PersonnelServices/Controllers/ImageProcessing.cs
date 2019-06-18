using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PersonnelServices.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PersonnelServices.Controllers
{    
    public class ImageProcessing
    {
        readonly string subscriptionKey = "1b571353cbdf4e97b9e341e88c118d55";
        readonly string uriBase = "https://centralus.api.cognitive.microsoft.com/face/v1.0/";
        readonly string requestParameters = "returnFaceId=true&returnFaceLandmarks=false" +
                "&returnFaceAttributes=emotion";

        public ImageProcessing(IConfiguration configuration)
        {
            subscriptionKey = configuration.GetSection("AzureConnectionStrings").GetValue<string>("subscriptionKey");
            uriBase = configuration.GetSection("AzureConnectionStrings").GetValue<string>("uriBase");

            if (string.IsNullOrEmpty(subscriptionKey))
            {
                Console.WriteLine("Error,Configuration file wasn't charged");
            }
        }
        
        public async Task<List<ModEmotion>> MakeAnalysisRequest(byte[] byteData)
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
                return JsonToEmotion(contentString);                 
            }
        }

        public List<ModEmotion> JsonToEmotion(string json)
        {
            dynamic jsonResponse = JsonConvert.DeserializeObject(json);

            if (jsonResponse.Count== 0)
            {
                return null;
            }

            List<ModEmotion> responseList = new List<ModEmotion>();

            for (int i = 0;i< jsonResponse.Count;i++)
            {
                var response = jsonResponse[i];

                var face = response["faceAttributes"];
                var faceId = response["faceId"];

                JObject emotion = face["emotion"];

                double max = double.MinValue;
                string key = string.Empty;

                foreach (var child in emotion)
                {
                    JToken val = child.Value;
                    float valf = (float)val;
                    if (valf > max)
                    {
                        key = child.Key;
                        max = valf;
                    }
                }

                ModEmotion modEmotion = new ModEmotion()
                {
                    Date = DateTime.UtcNow,
                    Details = emotion.ToString(),
                    Emotion = key,
                    Score = max.ToString(),
                    FaceId = faceId
                };
                responseList.Add(modEmotion);
            }

            return responseList;
        }

    }
}
