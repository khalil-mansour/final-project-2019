using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Controllers
{

    [ApiController]
    public class HealthzController : ControllerBase
    {
        [HttpGet("healthz")]
        public bool Get()
        {
            return true;
        }
    }
}
