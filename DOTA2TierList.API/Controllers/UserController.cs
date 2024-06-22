using Microsoft.AspNetCore.Mvc;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Models.TierListModel;
using DOTA2TierList.Application.Services;
using System;
using AutoMapper;
using FluentValidation;
using DOTA2TierList.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using DOTA2TierList.Infrastructure.Auth;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using DOTA2TierList.API.Contracts.UserContracts;
using DOTA2TierList.API.Contracts;

namespace DOTA2TierList.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        private readonly IMapper _mapper;

        private readonly IValidator<IUserRequest> _validator;

        public UserController(
            UserService userService, 
            IMapper mapper,
            IValidator<IUserRequest> validator)
        {
            _userService = userService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet("[action]/{id:long}")]
        [Authorize("Admin")]
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
        public async Task<ActionResult> Register(RegisterUserRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var user = _mapper.Map<User>(request);

            await _userService.Register(user);

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Login(LoginUserRequest request, [FromServices] IOptions<JwtOptions> options)
        {
            await _validator.ValidateAndThrowAsync(request);

            var user = _mapper.Map<User>(request);

            var token = await _userService.Login(user);

            Response.Cookies.Append(options.Value.CookieKey, token);

            return Ok();
        }
    }
}
