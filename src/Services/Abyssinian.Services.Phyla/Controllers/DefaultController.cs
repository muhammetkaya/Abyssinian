using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Abyssinian.Messaging;
using Abyssinian.Services.Phyla.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Abyssinian.Services.Phyla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly IPhylaManager _phylaManager;

        public DefaultController(IPhylaManager phylaManager)
        {
            _phylaManager = phylaManager;
        }

        /// <summary>
        /// Returns 500
        /// </summary>
        /// <returns>500</returns>
        [HttpGet("api/GetCountOfFamily")]
        public int GetCountOfFamily()
        {
            return 500;
        }

        /// <summary>
        /// Get consumed messaged.
        /// </summary>
        /// <returns>consumed messages</returns>
        [HttpGet("api/GetMessages")]
        public List<Message> GetMessages()
        {
            return _phylaManager.GetMessages();
        }

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="message">Message Content</param>
        /// <param name="to">Receiver</param>
        [HttpPost("api/SendMessage")]
        public void SendMessage([FromBody, Required]string message, [FromQuery, Required]string to)
        {
            _phylaManager.SendMessage(message, to);
        }
    }
}