using AutoMapper;
using MediatR;
using MicroService.Api.Autor.Models;
using MicroService.Api.Autor.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MicroService.Api.Autor.Application
{
    public class Consulta
    {

        public class ListaAutor : IRequest<List<AutorDTO>>
        {

        }

        public class Manejador : IRequestHandler<ListaAutor, List<AutorDTO>>
        {
            private readonly ContextAutor _context;
            private readonly IMapper _mapper;

            public Manejador(ContextAutor contexto, IMapper mapper)
            {
                _context = contexto;
                _mapper = mapper;
            }

            public async Task<List<AutorDTO>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                var autores = await _context.AutorLibro.ToListAsync();
                var autoresDTO = _mapper.Map<List<AutorLibro>, List<AutorDTO>>(autores);

                return autoresDTO;
            }

        }
    }
}
