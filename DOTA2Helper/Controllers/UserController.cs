using Microsoft.AspNetCore.Mvc;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Models.TierListModel;
using DOTA2TierList.Application.Contracts.UserContracts;
using DOTA2TierList.Application.Services;
using System;
using AutoMapper;
using FluentValidation;
using DOTA2TierList.Application.Contracts;

namespace DOTA2TierList.API.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("[action]/{id:long}")]
        public async Task<ActionResult> GetById(long id)
        {
            var user = await _userService.GetById(id);
            return Json(user);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetByEmail(string email)
        {
            var user = await _userService.GetByEmail(email);
            return Json(user);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Register([FromBody]RegisterUserRequest request)
        {

            await _userService.Register(request);

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Login([FromBody] LoginUserRequest request)
        {
            var token = await _userService.Login(request);

            return Ok(token);
        }
    }
}
