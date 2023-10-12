using AutoMapper;
using MicroService.Api.Libro.Application;
using MicroService.Api.Libro.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Api.Libro.Tests
{
    public class MappingTest : Profile
    {
        public MappingTest()
        {
            CreateMap<LibreriaMaterial, LibreriaDTO>();
        }
    }
}
