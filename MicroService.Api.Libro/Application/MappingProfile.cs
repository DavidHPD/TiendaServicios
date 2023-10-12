using AutoMapper;
using MicroService.Api.Libro.Models;

namespace MicroService.Api.Libro.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LibreriaMaterial, LibreriaDTO>();
        }
    }
}
