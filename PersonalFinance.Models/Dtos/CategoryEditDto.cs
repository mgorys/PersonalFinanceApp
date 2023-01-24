using PersonalFinance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Models.Dtos
{
    public class CategoryEditDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Color { get; set; }

    }
}
