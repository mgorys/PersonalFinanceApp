using AutoMapper;
using PersonalFinance.Abstractions.IServices;
using PersonalFinance.Entities;
using PersonalFinance.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Infrastructure.Mapping
{
    public class EntityMappingProfile : Profile
    {
        
        public EntityMappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<TransactionCreateDto, Transaction>();
            CreateMap<CategoryEditDto, Category>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<Transaction, TransactionDto>();
            CreateMap<RegisterDto, User>();
        }
        public string ChangeDateTimeFormat(DateTime date)
        {
            return date.ToString("yyyy-MM-dd hh:mm:ss");
        }
       
    }
}
