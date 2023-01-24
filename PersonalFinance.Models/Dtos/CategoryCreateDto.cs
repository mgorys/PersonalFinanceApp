using PersonalFinance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Models.Dtos
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public TypeOfCategory Type { get; set; }
        public string? Color { get; set; }
    }
}
