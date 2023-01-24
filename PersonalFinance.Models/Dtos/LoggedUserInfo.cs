using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Models.Dtos
{
    public class LoggedUserInfo
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
