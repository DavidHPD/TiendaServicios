using AutoMapper;
using MediatR;
using MicroService.Api.Libro.Models;
using MicroService.Api.Libro.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MicroService.Api.Libro.Application
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<LibreriaDTO>> { }

        public class Manejador : IRequestHandler<Ejecuta, List<LibreriaDTO>>
        {
            private readonly ContextoLibreria _context;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibreria contexto, IMapper mapper)
            { 
                _context = contexto;
                _mapper = mapper;
            }


            public async Task<List<LibreriaDTO>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await _context.libreriaMaterial.ToListAsync();
                var librosDTO =  _mapper.Map<List<LibreriaMaterial>, List<LibreriaDTO>>(libros);

                return librosDTO;


            }
        }
    }
}
