using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PromoCodes_main.Application.Commands;
using PromoCodes_main.Application.Models;
using PromoCodes_main.Application.Queries;
using PromoCodes_main.Infrastructure.Utility.Security;
using PromoCodes_main.Services;
using PromoCodes_main.Application.Entities;

namespace PromoCodes_main.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        // List<User> _users = new List<User>();
        // public UserController(List<User> users)
        // {
        //     this._users = users;
        // }

    
        /// <summary>
        /// Authenticate a user and generates an authorization token 
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        /// <summary>
        /// Gets lists of all users
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}