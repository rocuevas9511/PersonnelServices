using Microsoft.AspNetCore.Mvc;
using PersonnelServices.DAL.Interface;
using PersonnelServices.Model;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelServicesController : PersonnelBaseController
    {
        public PersonnelServicesController(IRepository repository)
            : base(repository)
        {

        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get una pruba del API",
            Description = "Solo es una prueba del API.",
            OperationId = "GetTest",
            Tags = new[] { "Test" }
        )]
        [SwaggerResponse(201, "Successfully")]
        [SwaggerResponse(204, "No records found")]
        [SwaggerResponse(500, "Something not expected happened.")]
        [Route("test")]
        public async Task<IActionResult> GetAllApplications()
        {
            IActionResult response;

            try
            {
                IEnumerable<ModTest> result = await _mongodb.ApiTest.GetTestsAsync();

                if (result.Count() > 0)
                {
                    response = Ok(result);
                }
                else
                {
                    response = NoContent();
                }
            }
            catch 
            {
                response = ResponseErrorCode();
            }

            return response;
        }
    }
}