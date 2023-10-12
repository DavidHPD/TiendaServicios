using MediatR;
using MicroService.Api.Autor.Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MicroService.Api.Autor.Models;
using FluentValidation;

namespace MicroService.Api.Autor.Application
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }

            public DateTime? FechaNacimiento { get; set; }

        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
            }

        }

            public class Manejador : IRequestHandler<Ejecuta>
            {

                public readonly ContextAutor _context;

                public Manejador(ContextAutor contexto)
                {
                    _context = contexto;
                }


                public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
                {
                    var autorLibro = new AutorLibro
                    {
                        Nombre = request.Nombre,
                        Apellido = request.Apellido,
                        FechaNacimiento = request.FechaNacimiento,
                        AutorLibroGuid = Convert.ToString(Guid.NewGuid())
                    };

                    _context.AutorLibro.Add(autorLibro);
                    var valor = await _context.SaveChangesAsync();

                    if (valor > 0)
                    {
                        return Unit.Value;
                    }

                    throw new Exception("No se pudo insertar el Autor");

                }

            }


        }
    }
