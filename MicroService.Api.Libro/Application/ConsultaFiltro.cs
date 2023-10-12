using AutoMapper;
using FluentValidation;
using MediatR;
using MicroService.Api.Libro.Models;
using MicroService.Api.Libro.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicroService.Api.Libro.Application
{
    public class ConsultaFiltro
    {

        public class Ejecuta : IRequest<LibreriaDTO> {
        
            public Guid? LibreriaMaterialId { get; set; }
        }


        public class EjecutaValidacion: AbstractValidator<Ejecuta>
        {

            public EjecutaValidacion()
            {
                RuleFor(x => x.LibreriaMaterialId).NotEmpty();
            }
        
        }

        public class Manejador : IRequestHandler<Ejecuta, LibreriaDTO>
        {
            private readonly ContextoLibreria _context;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibreria context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<LibreriaDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                
                var libroUnico = await _context.libreriaMaterial.Where(x => x.LibreriaMaterialId == request.LibreriaMaterialId).FirstOrDefaultAsync();

                if (libroUnico == null) { throw new Exception("Libro no encontrado"); }

                var libroDTO = _mapper.Map<LibreriaMaterial, LibreriaDTO>(libroUnico);

               

                return libroDTO;

                
            }
        }

    }
}
