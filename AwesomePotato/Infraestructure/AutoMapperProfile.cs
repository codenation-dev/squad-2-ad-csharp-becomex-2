using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomePotato.Models;
using AwesomePotato.DTOs;

namespace AwesomePotato
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ErrorLogData, ErrorLogDataDTO>().ReverseMap();
        }
    }
}
