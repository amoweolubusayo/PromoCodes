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
namespace PromoCodes_main.Controllers
{
    [ApiController]
    [Route("api/promo")]
    public class PromoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PromoController(IMediator mediator)
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
        public async Task<IActionResult> GetAll()
        {
            var query = new GetServicesQuery();
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
        public async Task<IActionResult> ActivateBonus([FromBody]ActivateBonusCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Status ? (IActionResult)Ok(result) : BadRequest(result);
        }

    }
}