using AutoMapper;
using DOTA2TierList.API.Contracts;
using DOTA2TierList.API.Contracts.UserContracts;
using DOTA2TierList.API.Controllers;
using DOTA2TierList.API.Mapping;
using DOTA2TierList.API.Validation;
using DOTA2TierList.Application.Services;
using DOTA2TierList.Application.Services.ServiceInterfaces;
using DOTA2TierList.Infrastructure.Auth;
using DOTA2TierList.Logic.Models;
using FluentValidation;
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

        private User GetUserById(long id)   
        {
            var users = new User[] {
                new User { Id = 1, Name = "Denis" }, 
                new User { Id = 2, Name = "Vladimir"}, 
                new User { Id = 3, Name = "Dmirty" }
            };


            return users.SingleOrDefault(u => u.Id == id)!;
        }
        [Theory]
        [InlineData(1, "Denis")]
        [InlineData(3, "Dmirty")]
        [InlineData(2, "Vladimir")]
        public async Task UserGetById(long id, string expected)
        {
            // arrange
            _userServiceMock.Setup(store => store.GetById(It.IsAny<long>()))
            .ReturnsAsync((long id) => GetUserById(id));

            var controller = new UserController(_userServiceMock.Object, _mapper, _validator, _jwtOptions);
            // act
            var result = await controller.GetById(id);


            // assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            var json = JsonSerializer.Serialize(jsonResult.Value);
            var response = JsonSerializer.Deserialize<UserResponse>(json);
            Assert.Equal(expected, response!.Name);
        }
    }
}
