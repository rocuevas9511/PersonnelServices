using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2.HPack;
using Microsoft.Extensions.Configuration;
using PersonnelServices.DAL.Interface;

namespace PersonnelServices.Controllers
{
    [Produces("application/json")]
    [Route("api/personnel")]
    [ApiController]
    public class PersonnelBaseController : ControllerBase
    {
        protected readonly IConfiguration Configuration;
        protected readonly IRepository _mongodb;

        public PersonnelBaseController(IRepository repository, IConfiguration configuration)
        {
            Configuration = configuration;

            if (repository != null)
            {
                _mongodb = repository;
            }
        }

        protected virtual IActionResult ResponseErrorCode( )
        {
            return StatusCode(500, "Something not expected happened");
        }
    }
}