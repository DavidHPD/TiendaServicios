using AutoMapper;
using MicroService.Api.Autor.Models;

namespace MicroService.Api.Autor.Application
{
    public class MappingProfile : Profile 
    {
        public MappingProfile() {
            CreateMap<AutorLibro, AutorDTO>();
        }

    }
}
