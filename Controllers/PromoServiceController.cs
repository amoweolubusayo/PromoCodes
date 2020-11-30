using System;
using System.Data;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PromoCodes_main.Application.Queries;
using PromoCodes_main.Application.Commands;
using PromoCodes_main.Application.Models;
namespace PromoCodes_main.Controllers
{
    [ApiController]
    [Route("api/promo")]
    public class PromoServiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PromoServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets all promo services
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("allservices")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetServicesQuery();
            var result = await _mediator.Send(query);
            return result.Status ? (IActionResult)Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Searches services by name
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("searchservicebyname")]
        [Authorize]
        public async Task<IActionResult> SearchServiceByName(string serviceName)
        {
            var query = new GetServiceByNameQuery
            {
                ServiceName = serviceName
            };
            var result = await _mediator.Send(query);
            return result.Status ? (IActionResult)Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Activates Bonus
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost("activatebonus")]
        [Authorize]
        public async Task<IActionResult> ActivateBonus([FromBody] ActivateBonusCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Status ? (IActionResult)Ok(result) : BadRequest(result);
        }

         /// <summary>
        /// dectivates bonus (test purposes)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost("deactivatebonus")]
        [Authorize]
        public async Task<IActionResult> DeActivateBonus([FromBody] DeactivateBonusCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Status ? (IActionResult)Ok(result) : BadRequest(result);
        }

    }
}