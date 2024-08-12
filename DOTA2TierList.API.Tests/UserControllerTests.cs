using AutoMapper;
using DOTA2TierList.API.Contracts;
using DOTA2TierList.API.Contracts.UserContracts;
using DOTA2TierList.API.Controllers;
using DOTA2TierList.API.Mapping;
using DOTA2TierList.API.Validation;
using DOTA2TierList.Application.Contracts;
using DOTA2TierList.Application.Services;
using DOTA2TierList.Application.Services.ServiceInterfaces;
using DOTA2TierList.Infrastructure.Auth;
using DOTA2TierList.Logic.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace DOTA2TierList.API.Tests
{
    public class UserControllerTest
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly IMapper _mapper;
        private readonly IValidator<IUserRequest> _validator;
        private readonly IOptions<JwtOptions> _jwtOptions;

        public UserControllerTest() 
        { 
            _userServiceMock = new Mock<IUserService>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<DtoUserMappingProfile>()));
            _validator = new UserRequestValidator();
            _jwtOptions = Options.Create(
                new JwtOptions
                {
                    CookieAccessKey = "access",
                    ExpiresAccessTokenSeconds = 10,
                    ExpiresRefreshTokenSeconds = 70,
                    CookieRefreshKey = "refresh",
                    SecretKey = "ahahahhaha"
                });
        }

        private User[] GetUsers()   
        {
            return [
                new User { Id = 1, Name = "Denis", Email = "Vadimkings@yandex.ru" },
                new User { Id = 2, Name = "Vladimir", Email = "Spiners@yandex.ru"},
                new User { Id = 3, Name = "Dmirty", Email = "Pudgersor@mail.ru" }
            ];
        }

        private LoginUserRequest[] GetLoginUserRequests()
        {
            return [
                new LoginUserRequest("Vadimkings@yandex.ru", "satyidfsd"),
                new LoginUserRequest("Spers@yandex.ru", "sashfghjd"),
                new LoginUserRequest("Pudgersor@mail.ru", "sddsgsdgha")
            ];
        }

        [Theory]
        [InlineData(1, "Denis")]
        [InlineData(3, "Dmirty")]
        [InlineData(2, "Vladimir")]
        public async Task UserGetById(long id, string expected)
        {
            // arrange
            _userServiceMock.Setup(store => store.GetById(It.IsAny<long>()))
            .ReturnsAsync((long id) => 
            GetUsers().SingleOrDefault(u => u.Id == id)!);

            var controller = new UserController(_userServiceMock.Object, _mapper, _validator, _jwtOptions);
            // act
            var result = await controller.GetById(id);


            // assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            var json = JsonSerializer.Serialize(jsonResult.Value);
            var response = JsonSerializer.Deserialize<UserResponse>(json);
            Assert.Equal(expected, response!.Name);
        }

        [Theory]
        [InlineData("Vadimkings@yandex.ru", "Denis")]
        [InlineData("Pudgersor@mail.ru", "Dmirty")]
        [InlineData("Spiners@yandex.ru", "Vladimir")]
        public async Task UserGetByEmail(string email, string expected)
        {
            // arrange
            _userServiceMock.Setup(store => store.GetByEmail(It.IsAny<string>()))
            .ReturnsAsync((string email) => 
            GetUsers().SingleOrDefault(u => u.Email == email)!);

            var controller = new UserController(_userServiceMock.Object, _mapper, _validator, _jwtOptions);
            // act
            var result = await controller.GetByEmail(email);


            // assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            var json = JsonSerializer.Serialize(jsonResult.Value);
            var response = JsonSerializer.Deserialize<UserResponse>(json);
            Assert.Equal(expected, response!.Name);
        }

        [Theory]
        [InlineData("Vadimkings@yandex.ru", true)]
        [InlineData("Pudgersor@mail.ru", true)]
        [InlineData("Spers@yandex.ru", false)]
        public async Task UserLogin(string email, bool expected)
        {
            // arrange
            var request = GetLoginUserRequests().SingleOrDefault(u => u.Email == email);

            _userServiceMock.Setup(store => store.Login(It.IsAny<User>()))
            .ReturnsAsync((User user) =>
            {
                var existedUser = GetUsers().FirstOrDefault(u => u.Email == user.Email);

                if (existedUser != null)
                {
                    return ("accessToken", "refreshToken");
                }

                return (null, null);
            });

            var controller = new UserController(_userServiceMock.Object, _mapper, _validator, _jwtOptions);
            var httpContext = new DefaultHttpContext();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // act
            await controller.Login(request!);


            // assert
            var setCookieHeaders = httpContext.Response.Headers["Set-Cookie"];
            var cookieString = setCookieHeaders.FirstOrDefault(header => header.StartsWith("access="));

            if (expected)
            {
                Assert.False(string.IsNullOrWhiteSpace(cookieString), "Cookie was not set.");
            }
            else
            {
                Assert.True(string.IsNullOrWhiteSpace(cookieString), "Cookie was not set.");
            }
                
        }


    }
}
