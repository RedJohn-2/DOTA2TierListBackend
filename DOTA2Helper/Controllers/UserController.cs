using Microsoft.AspNetCore.Mvc;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Models.TierListModel;
using DOTA2TierList.API.Contracts;
using DOTA2TierList.Application.Services;
using DOTA2TierList.Application.Mappers;

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

        [HttpGet("{id:long}")]
        public async Task<ActionResult> GetById(long id)
        {
            var user = await _userService.GetById(id);
            var response = _userMapper.ToUserResponse(user);
            return Json(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetByEmail(string email)
        {
            var user = await _userService.GetByEmail(email);
            var response = _userMapper.ToUserResponse(user);
            return Json(response);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateUserRequest request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = request.PasswordHash
            };

            await _userService.Create(user);

            return Ok();
        }
    }
}
