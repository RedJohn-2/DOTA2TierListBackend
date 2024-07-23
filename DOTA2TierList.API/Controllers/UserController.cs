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

namespace DOTA2TierList.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        private readonly IMapper _mapper;

        private readonly IValidator<IUserRequest> _validator;

        private readonly Regex _accountIdRegex = new Regex(@"^http://steamcommunity\.com/openid/id/(7[0-9]{15,25})$", RegexOptions.Compiled);

        public UserController(
            UserService userService,
            IMapper mapper,
            IValidator<IUserRequest> validator)
        {
            _userService = userService;
            _mapper = mapper;
            _validator = validator;
        }


        [HttpPost("[action]")]
        public IActionResult SignInSteam([FromBody]string returnUrl)
        {

            return Json(_userService.SteamAuth(returnUrl));
        }

        

        [HttpPost("[action]")]
        public async Task<IActionResult> VerifySteam()
        {
            var request = HttpContext.Request;
            //Here we can retrieve the claims
            // read external identity from the temporary cookie
            //var authenticateResult = HttpContext.GetOwinContext().Authentication.AuthenticateAsync("ExternalCookie");
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (result.Succeeded != true)
            {
                throw new Exception("External authentication error");
            }

            // retrieve claims of the external user
            var externalUser = result.Principal;
            if (externalUser == null)
            {
                throw new Exception("External authentication error");
            }

            // retrieve claims of the external user
            var claims = externalUser.Claims.ToList();

            // try to determine the unique id of the external user - the most common claim type for that are the sub claim and the NameIdentifier
            // depending on the external provider, some other claim type might be used
            //var userIdClaim = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject);
            var userIdClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new Exception("Unknown userid");
            }

            var externalUserId = userIdClaim.Value;
            var externalProvider = userIdClaim.Issuer;

            // use externalProvider and externalUserId to find your user, or provision a new user

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
        public async Task<ActionResult> Login(LoginUserRequest request, [FromServices] IOptions<JwtOptions> options)
        {
            await _validator.ValidateAndThrowAsync(request);

            var user = _mapper.Map<User>(request);

            (var accessToken, var refreshToken) = await _userService.Login(user);

            Response.Cookies.Append(options.Value.CookieAccessKey, accessToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(7)
                });

            Response.Cookies.Append(options.Value.CookieRefreshKey, refreshToken,
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
