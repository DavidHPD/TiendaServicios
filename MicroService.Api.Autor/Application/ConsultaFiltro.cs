using AutoMapper;
using MediatR;
using MicroService.Api.Autor.Models;
using MicroService.Api.Autor.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicroService.Api.Autor.Application
{
    public class ConsultaFiltro
    {
        public class AutorUnico : IRequest<AutorDTO> { 
         
            public string AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDTO>
        {

            private readonly ContextAutor _context;
            private readonly IMapper _mapper;

            public Manejador(ContextAutor context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;   
            }

           

            async Task<AutorDTO> IRequestHandler<AutorUnico, AutorDTO>.Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await _context.AutorLibro.Where(a => a.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync();

                if (autor == null)
                {
                    throw new Exception("No se encontro el autor");
                }

                var autorDTO = _mapper.Map<AutorLibro, AutorDTO>(autor);

                return autorDTO;
            }
        }
    }
}
