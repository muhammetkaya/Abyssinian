using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Abyssinian.Services.Phyla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        /// <summary>
        /// Returns 500
        /// </summary>
        /// <returns>500</returns>
        [HttpGet("api/GetCountOfFamily")]
        public int GetCountOfFamily()
        {
            return 500;
        }
    }
}