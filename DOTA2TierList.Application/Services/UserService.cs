using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DOTA2TierList.Application.Contracts;
using DOTA2TierList.Application.Contracts.UserContracts;
using DOTA2TierList.Application.Interfaces.Auth;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Store;
using FluentValidation;

namespace DOTA2TierList.Application.Services
{
    public class UserService
    {
        private readonly IUserStore _userStore;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly IValidator<IUserRequest> _validator;
        private readonly IJwtProvider _jwtProvider;

        public UserService(
            IUserStore userStore, 
            IPasswordHasher passwordHasher,
            IValidator<IUserRequest> validator,
            IMapper mapper,
            IJwtProvider jwtProvider)
        {
            _userStore = userStore;
            _passwordHasher = passwordHasher;
            _validator = validator;
            _mapper = mapper;
            _jwtProvider = jwtProvider;
        }

        public async Task Register(RegisterUserRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);
           
            var existedUser = await _userStore.GetByEmail(request.Email);

            if (existedUser != null)
            {
                throw new Exception();
            }

            var hash = _passwordHasher.Hash(request.Password);

            var user = _mapper.Map<User>(request, opt => opt.Items["PasswordHash"] = hash);

            await _userStore.Create(user);
        }

        public async Task<string> Login(LoginUserRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var user = await _userStore.GetByEmail(request.Email);

            if (user == null)
            {
                throw new Exception();
            }


            if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                throw new Exception();   
            }

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }

        public async Task<UserResponse> GetById(long id)
        {
            var user = await _userStore.GetById(id);

            if (user == null)
            {
                throw new Exception();
            }

            var response = _mapper.Map<UserResponse>(user);

            return response;
        }

        public async Task<UserResponse> GetByEmail(string email)
        {
            var user = await _userStore.GetByEmail(email);

            if (user == null)
            {
                throw new Exception();
            }

            var response = _mapper.Map<UserResponse>(user);

            return response;
        }
    }
}
