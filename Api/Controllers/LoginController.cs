using System;
using System.Collections.Generic;
using System.Reflection;
using App.Api.Controllers;
using Core.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    public class LoginController : BaseController
    {
        /// <summary>
        /// Login Uset and generated access_token
        /// </summary>
        /// <returns>Create access_token</returns>
        /// <response code="200">Create access_token</response>
        /// <response code="400">Return another error</response>
        /// <response code="422">Model validation error</response>
        [HttpPost("/")]
        [ProducesResponseType(typeof(LoginResponse), 200)]
        public IActionResult Index(LoginRequest loginModel)
        {
            return Ok(JsonConvert.SerializeObject(new {}));
        }
    }
}