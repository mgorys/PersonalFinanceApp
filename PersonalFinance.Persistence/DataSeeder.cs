using PersonalFinance.Abstractions.IRepositories;
using PersonalFinance.Abstractions.IServices;
using PersonalFinance.Entities;
using PersonalFinance.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Persistence
{
    public class DataSeeder
    {
        private readonly PFDbContext _context;
        private readonly IAccountService _accountService;

        public DataSeeder(PFDbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public async void Seed()
        {
            if (!_context.Database.CanConnect())
                return;
            if (!_context.Users.Any())
            {
                await RegisterUser();
                _context.SaveChanges();
            }
            if (!_context.Categories.Any())
            {
                _context.Categories.AddRange(GetCategories());
                _context.SaveChanges();
            }
            if (!_context.Transactions.Any())
            {
                _context.Transactions.AddRange(GetTransactions());
                _context.SaveChanges();
            }
        }
        private async Task<bool> RegisterUser()
        {
            var user = new RegisterDto()
            {
                Email = "string@string.com",
                Password = "12345",
                ConfirmPassword = "12345",
                Name = "Antony"
            };
            await _accountService.RegisterUserAsync(user);
            return true;
        }
        private IEnumerable<Category> GetCategories()
        {
            return new List<Category>()
                {
                new Category() { Name = "Food" , Type = TypeOfCategory.EXPENDITURE, UserId = 1 , Color = "#FF0000", Default = false },
                new Category() { Name = "Living" , Type = TypeOfCategory.EXPENDITURE, UserId = 1 , Color = "#FFF700", Default = false },
                new Category() { Name = "Uncategorized Expenditure" , Type = TypeOfCategory.EXPENDITURE,UserId = 1 , Color = "#37FF00",Default= true },
                new Category() { Name = "Salary" , Type = TypeOfCategory.INCOME,UserId = 1 , Color = "#0008FF", Default = false},
                new Category() { Name = "Scholarship" , Type = TypeOfCategory.INCOME,UserId = 1 , Color = "#00FFFB", Default = false},
                new Category() { Name = "Uncategorized Income" , Type = TypeOfCategory.INCOME,UserId = 1 , Color = "#EE00FF", Default= true},
            };
        }
        private IEnumerable<Transaction> GetTransactions()
        {
            return new List<Transaction>()
                {
                    new Transaction() {Name="January",Amount = 2000, CategoryId=4,PutTime=DateTime.Now},
                    new Transaction() {Name="Rent", Amount=1000, CategoryId=2,PutTime=DateTime.Now},
                    new Transaction() {Name="Pizza", Amount=50, CategoryId=1,PutTime=DateTime.Now},
                    new Transaction() {Name="Sushi", Amount=100, CategoryId=1,PutTime=DateTime.Now.AddMonths(2)},
                    new Transaction() {Name="Kebab", Amount=30, CategoryId=1,PutTime=DateTime.Now.AddMonths(3)},
                    new Transaction() {Name="Mac", Amount=35, CategoryId=1,PutTime=DateTime.Now.AddMonths(6)},
                };
        }
    }
}
