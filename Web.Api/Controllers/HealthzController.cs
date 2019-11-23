using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HealthzController : ControllerBase
    {
        [HttpGet]
        public bool Get()
        {
            return true;
        }
    }
}
