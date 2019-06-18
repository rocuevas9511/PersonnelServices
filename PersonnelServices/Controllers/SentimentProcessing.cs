using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace PersonnelServices.Controllers
{
    public class SentimentProcessing
    {
        private readonly string subscriptionKey;
        private readonly string uriBase;
        private IConfiguration Configuration;

        public SentimentProcessing(IConfiguration configuration)
        {
            Configuration = configuration;

            subscriptionKey = Configuration.GetSection("SentimentConnectionStrings").GetValue<string>("subscriptionKey");
            uriBase = Configuration.GetSection("SentimentConnectionStrings").GetValue<string>("uriBase");
        }

        public Task<double> SentimentRequest(string text, string lang)
        {
            var client = new WebClient();
            client.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            client.Headers.Add("Content-Type", "application/json");
            client.Headers.Add("Accept", "application/json");

            // Determine sentiment
            var postData = @"{""documents"":[{""id"":""1"", ""language"":""@language"", ""text"":""@sampleText""}]}".Replace("@sampleText", text).Replace("@language", lang);
            var response = client.UploadString(uriBase, postData);

            var sentimentStr = new Regex(@"""score"":([\d.]+)").Match(response).Groups[1].Value;

            return  Task.FromResult(Convert.ToDouble(sentimentStr, System.Globalization.CultureInfo.InvariantCulture));
        }
    }
}
