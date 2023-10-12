using FluentValidation;
using MediatR;
using MicroService.Api.Libro.Models;
using MicroService.Api.Libro.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MicroService.Api.Libro.Application
{
    public class Nuevo 
    {
        public class Ejecuta : IRequest
        { 
        
            public string Titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public Guid? AutorLibro { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta> { 
        
            public EjecutaValidacion() {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
                RuleFor(x => x.AutorLibro).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoLibreria _context;

            public Manejador(ContextoLibreria context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libro = new LibreriaMaterial { 
                 Titulo = request.Titulo,
                 FechaPublicacion = request.FechaPublicacion,
                 AutorLibro = request.AutorLibro

                };

                 _context.libreriaMaterial.Add(libro);
                var value = await _context.SaveChangesAsync();
                
                if(value > 0) { return Unit.Value; }

                throw new Exception("No se pudo insertar el libro");
                
            }
        }
    }
}
