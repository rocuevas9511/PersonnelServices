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

                        List<ModEmotion> resultList = await imageProcessingService.MakeAnalysisRequest(fileBytes);
                        List<bool> inserList = new List<bool>();
                        if (resultList != null)
                        {          
                            foreach(var emotion in resultList)
                            {
                                inserList.Add( await _mongodb.ApiEmotion.InsertEmotion(emotion));
                            }

                            if(inserList.TrueForAll(element=>element))
                                return Ok(resultList);
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
