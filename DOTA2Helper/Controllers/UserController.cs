using Microsoft.AspNetCore.Mvc;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Models.TierListModel;
using DOTA2TierList.API.Contracts;
using DOTA2TierList.Application.Services;
using DOTA2TierList.Application.Mappers;
using System;

namespace DOTA2TierList.API.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        public readonly UserService _userService;
        public readonly UserMapper _userMapper;

        public UserController(UserService userService, UserMapper userMapper)
        {
            _userService = userService;
            _userMapper = userMapper;
        }

        [HttpGet("[action]/{id:long}")]
        public async Task<ActionResult> GetById(long id)
        {
            var user = await _userService.GetById(id);
            var response = _userMapper.ToUserResponse(user);
            return Json(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetByEmail(string email)
        {
            var user = await _userService.GetByEmail(email);
            var response = _userMapper.ToUserResponse(user);
            return Json(response);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Create([FromBody]CreateUserRequest request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = request.Password
            };

            await _userService.Create(user);

            return Ok();
        }
    }
}
