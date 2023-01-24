using PersonalFinance.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Models.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string? Description { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryId { get; set; }
        public DateTime PutTime { get; set; }
    }
}
