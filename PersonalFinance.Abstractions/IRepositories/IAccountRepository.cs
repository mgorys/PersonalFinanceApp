using PersonalFinance.Entities;
using PersonalFinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Abstractions.IRepositories
{
    public interface IAccountRepository
    {
        Task<ServerResponseSuccess<int>> RegisterUserAsync(User registerUser, string password);
        Task<ServerResponseSuccess<User>> FindUserAsync(string loginUser);
    }
}
