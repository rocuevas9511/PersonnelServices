using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PersonnelServices.DAL.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelServices.Controllers
{
    public class ImageRecognitionController : PersonnelBaseController 
    {
        public ImageRecognitionController(IRepository repository, IConfiguration configuration)
            : base(repository, configuration)
        {

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

                        string result = await ImageProcessing.MakeAnalysisRequest(fileBytes);

                        if (!string.IsNullOrEmpty(result))
                        {
                            return Ok(result);
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
