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
