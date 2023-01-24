using PersonalFinance.Models.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Models.Dtos
{
    public class TransactionQuery
    {
        public string? SearchPhrase { get; set; }
        public string? Search {  get; set; }
        public int? Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? SortBy { get; set; }
        public SortDirection? SortDirection { get; set; }
    }
    
}
