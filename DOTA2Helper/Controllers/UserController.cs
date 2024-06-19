using Microsoft.AspNetCore.Mvc;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Models.TierListModel;
using DOTA2TierList.API.Contracts;
using DOTA2TierList.Application.Services;
using System;
using AutoMapper;

namespace DOTA2TierList.API.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        public readonly UserService _userService;
        public readonly IMapper _mapper;

        public UserController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("[action]/{id:long}")]
        public async Task<ActionResult> GetById(long id)
        {
            var user = await _userService.GetById(id);
            var response = _mapper.Map<UserResponse>(user);
            return Json(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetByEmail(string email)
        {
            var user = await _userService.GetByEmail(email);
            var response = _mapper.Map<UserResponse>(user);
            return Json(response);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Create([FromBody]CreateUserRequest request)
        {
            var user = _mapper.Map<User>(request);

            await _userService.Create(user);

            return Ok();
        }
    }
}
