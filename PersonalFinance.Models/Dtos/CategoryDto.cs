using PersonalFinance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Models.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TypeOfCategory Type { get; set; }
        public string? Color { get; set; }
        public int Amount { get; set; } = 0;
    }
}
