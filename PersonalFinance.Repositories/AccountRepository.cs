using Microsoft.EntityFrameworkCore;
using PersonalFinance.Entities;
using PersonalFinance.Models.Dtos;
using PersonalFinance.Models;
using PersonalFinance.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinance.Abstractions.IRepositories;
using Microsoft.AspNetCore.Identity;

namespace PersonalFinance.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly PFDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountRepository(PFDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }
        public async Task<ServerResponseSuccess<int>> RegisterUserAsync(User registerUser, string password)
        {
            var response = new ServerResponseSuccess<int>();
            var newUser = new User()
            {
                Email = registerUser.Email,
                Name = registerUser.Name,
            };

            if (registerUser == null)
            {
                response.Success = false;
            }
            else
            {
                var hashedPassword = _passwordHasher.HashPassword(newUser, password);
                newUser.PasswordHash = hashedPassword;
                await _dbContext.Users.AddAsync(newUser);
                await _dbContext.SaveChangesAsync();
                var result = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == newUser.Email);
                response.DataFromServer = result.Id;
                response.Success = true;
                
            }
            return response;
        }
        public async Task<ServerResponseSuccess<User>> FindUserAsync(string loginUser)
        {
            var response = new ServerResponseSuccess<User>();
            var result = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == loginUser);
            if (result == null)
            {
                response.Success = false;
                return response;
            }
            response.Success = true;
            response.DataFromServer = result;
            return response;
        }
       
    }
}
