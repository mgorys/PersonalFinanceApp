using PersonalFinance.Entities;
using PersonalFinance.Models;
using PersonalFinance.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Abstractions.IServices
{
    public interface IAccountService
    {
        Task RegisterUserAsync(RegisterDto registerUser);
        Task<string> GenerateJwt(LoginDto dto);
        Task<LoggedUserInfo> LoginUserAsync(LoginDto login);
        Task<ServerResponseSuccess<User>> FindUserAsync(string email);
    }
}
