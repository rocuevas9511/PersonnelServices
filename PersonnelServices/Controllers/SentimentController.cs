using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PersonnelServices.DAL.Interface;

namespace PersonnelServices.Controllers
{
    public class SentimentController : PersonnelBaseController
    {
        public SentimentController(IRepository repository, IConfiguration configuration)
            : base(repository, configuration)
        {

        }

        [HttpGet]
        [Route("sentiment/{text}/language/{language}")]
        public async Task<IActionResult> PostSentiment(string text, string language)
        {
            try
            {
                if (text.Length > 0)
                {
                    if (language.Length == 0)
                    {
                        language = "es";
                    }

                    SentimentProcessing sentiment = new SentimentProcessing(Configuration);

                    double result = await sentiment.SentimentRequest(text, language).ConfigureAwait(false);

                    if (result > 0)
                    {
                        await _mongodb.ApiSentiment.InsertSentiment(new Model.ModSentiments { Date = DateTime.UtcNow.ToString(), Score = result.ToString(), Sentiment = result > 0.5 ? "good" : "bad" });

                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, $"Server Internal Error {ex}");
            }
        }
    }
}