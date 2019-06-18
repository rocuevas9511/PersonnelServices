using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PersonnelServices.DAL.Interface;
using PersonnelServices.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelServices.Controllers
{
    public class ImageRecognitionController : PersonnelBaseController 
    {
        ImageProcessing imageProcessingService;
        public ImageRecognitionController(IRepository repository, IConfiguration configuration)
            : base(repository, configuration)
        {
            imageProcessingService = new ImageProcessing(configuration);
        }

        [HttpPost]
        [Route("postimage")]
        public async Task<IActionResult> PostImage([FromForm(Name = "file")] IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();

                        ModEmotion result = await imageProcessingService.MakeAnalysisRequest(fileBytes);

                        if (result!=null)
                        {                            
                            bool insert = await _mongodb.ApiEmotion.InsertEmotion(result);

                            if(insert)
                                return Ok(result);
                            else
                                return StatusCode(500, $"Server Internal Error, The object wasn't saved");
                        }
                        else
                        {
                            return NoContent();
                        }
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
