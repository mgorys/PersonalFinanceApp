using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PersonalFinance.Abstractions.IRepositories;
using PersonalFinance.Abstractions.IServices;
using PersonalFinance.Entities;
using PersonalFinance.Infrastructure.Exceptions;
using PersonalFinance.Models;
using PersonalFinance.Models.Dtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly ICategoryService _categoryService;

        public AccountService(IAccountRepository accountRepository, IMapper mapper, IPasswordHasher<User> passwordHasher,
            AuthenticationSettings authenticationSettings, ICategoryService categoryService)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _categoryService = categoryService;
        }
        public async Task RegisterUserAsync(RegisterDto registerUser)
        {
            string password = registerUser.Password;
            var newUser = _mapper.Map<User>(registerUser);
            var result = await _accountRepository.RegisterUserAsync(newUser, password);
            if (result.Success == false)
                throw new BadRequestException("Sorry, something went wrong");
            if(registerUser.Email != "string@string.com")
            await _categoryService.RegisterCreateCategories(result.DataFromServer);
        }
        public async Task<LoggedUserInfo> LoginUserAsync(LoginDto login)
        {
            var userName = await FindUserAsync(login.Email);
            if (userName.DataFromServer is null)
            {
                throw new BadRequestException("Invalid username or password");
            }
            var token = await GenerateJwt(login);
            var user = new LoggedUserInfo
            {
                Email = userName.DataFromServer.Email,
                UserName = userName.DataFromServer.Name,
                Token = token
            };
            return user;
        }
        public async Task<string> GenerateJwt(LoginDto login)
        {
            var user = await FindUserAsync(login.Email);

            if (user.DataFromServer is null)
            {
                throw new BadRequestException("Invalid username or password");
            }
            var result = _passwordHasher.VerifyHashedPassword(user.DataFromServer, user.DataFromServer.PasswordHash, login.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.DataFromServer.Id.ToString()),
                new Claim(ClaimTypes.Email, $"{user.DataFromServer.Email}"),

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
        public async Task<ServerResponseSuccess<User>> FindUserAsync(string email)
        {
            return await _accountRepository.FindUserAsync(email);
        }
    }
}
