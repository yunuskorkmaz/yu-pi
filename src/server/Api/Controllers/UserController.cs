using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Api.Controllers;
using AutoMapper;
using Core.Dtos.User;
using Core.Entities;
using Core.Services;
using Core.Shared;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        private readonly AppDbContext context;
        protected readonly IMapper mapper;
        public UserController(IUserService _userService,IMapper _mapper, AppDbContext _context)
        {
            userService = _userService;
            context = _context;
            mapper = _mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserModel), 200)]
        public async Task<ActionResult<UserModel>> Add(AddUserModel model)
        {
            var user = mapper.Map<User>(model);
            user = await userService.AddUser(user);
            var response = mapper.Map<UserModel>(user);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<UserModel>), 200)]
        public async Task<ActionResult<List<UserModel>>> GetUserList()
        {
            var result = await userService.GetUserList();
            var response = mapper.Map<List<UserModel>>(result);
            return Ok(response);
        }
    }
}