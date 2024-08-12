using Microsoft.AspNetCore.Mvc;
using DOTA2TierList.Logic.Models;
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
using Microsoft.AspNetCore.Authentication;
using AspNet.Security.OpenId.Steam;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using RestSharp;
using AngleSharp.Common;
using DOTA2TierList.Application.Services.ServiceInterfaces;

namespace DOTA2TierList.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        private readonly IValidator<IUserRequest> _validator;

        private readonly JwtOptions _jwtOptions;

        public UserController(
            IUserService userService,
            IMapper mapper,
            IValidator<IUserRequest> validator,
            IOptions<JwtOptions> options)
        {
            _userService = userService;
            _mapper = mapper;
            _validator = validator;
            _jwtOptions = options.Value;
        }


        [HttpPost("[action]")]
        public IActionResult SignInSteam(string returnUrl)
        {
            return Json(_userService.SteamAuth(returnUrl));
        }

        

        [HttpGet("[action]")]
        public async Task<IActionResult> VerifySteam()
        {
            var queryParams = Request.QueryString;

            var verifyAuthUrl = _userService.GetVerifyAuthUrl(queryParams.ToString());

            if (verifyAuthUrl == null) { 
                return Unauthorized();
            }

            using var client = new HttpClient();
            try
            {
                var verifyResult = await client.GetAsync(verifyAuthUrl);

                if (verifyResult.IsSuccessStatusCode)
                {
                    var content = await verifyResult.Content.ReadAsStringAsync();
                    var isAuth = _userService.IsSuccessSteamAuth(content);


                    if (!isAuth)
                    {
                        return Unauthorized();
                    }

                    else
                    {
                        await SteamLogin(Request.Query["openid.claimed_id"]!);

                        return Ok();
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch
            {
                return Unauthorized();
            }


        }
        private async Task<ActionResult> SteamLogin(string claimedIdUrl)
        {

            (var accessToken, var refreshToken) = await _userService.SteamLogin(claimedIdUrl);

            Response.Cookies.Append(_jwtOptions.CookieAccessKey, accessToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(7)
                });

            Response.Cookies.Append(_jwtOptions.CookieRefreshKey, refreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(7)
                });

            return Ok();
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
        public async Task<ActionResult> Register(RegisterUserRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var user = _mapper.Map<User>(request);

            await _userService.Register(user);

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Login(LoginUserRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var user = _mapper.Map<User>(request);

            (var accessToken, var refreshToken) = await _userService.Login(user);

            Response.Cookies.Append(_jwtOptions.CookieAccessKey, accessToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(7)
                });

            Response.Cookies.Append(_jwtOptions.CookieRefreshKey, refreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(7)
                });

            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize(Policy = "User")]
        public ActionResult Logout([FromServices] IOptions<JwtOptions> options)
        {

            Response.Cookies.Delete(options.Value.CookieAccessKey);

            Response.Cookies.Delete(options.Value.CookieRefreshKey);

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Refresh([FromServices] IOptions<JwtOptions> options)
        {

            var accessToken = Request.Cookies[options.Value.CookieAccessKey];

            var refreshToken = Request.Cookies[options.Value.CookieRefreshKey];

            if (accessToken is null || refreshToken is null)                        
                return Unauthorized();

            (var newAccessToken, var newRefreshToken) = await _userService.Refresh(accessToken!, refreshToken!);

            Response.Cookies.Append(options.Value.CookieAccessKey, newAccessToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(7)
                });

            Response.Cookies.Append(options.Value.CookieRefreshKey, newRefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(7)
                });

            return Ok();
        }

        [HttpPut("[action]")]
        [Authorize]
        public async Task<ActionResult> Update(UpdateUserRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);
            var userId = long.Parse(User.Claims.FirstOrDefault(i => i.Type == "userId")!.Value);

            await _userService.Update(userId, request.Name, request.Email);

            return Ok();
        }


    }
}
