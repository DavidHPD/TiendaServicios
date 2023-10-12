using AutoMapper;
using MediatR;
using MicroService.Api.CarritoCompra.Models;
using MicroService.Api.CarritoCompra.Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MicroService.Api.CarritoCompra.Application
{
    public class Nuevo
    {
        public class Ejecuta : IRequest { 
        
            public DateTime? FechaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {

            private readonly ContextoCarrito _context;

            public Manejador(ContextoCarrito context)
            {
                _context = context;
                
            }


            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion { 
                    FechaCreacion = request.FechaCreacionSesion
                };

                _context.CarritoSesion.Add(carritoSesion);
                var value = await _context.SaveChangesAsync();

                if (value == 0) {
                    throw new Exception("Errores en la inserción del carrito");
                }

               int id = carritoSesion.CarritoSesionId;

                foreach (var obj in request.ProductoLista)
                {
                    var detalleSesion = new CarritoSesionDetalle { 
                    FechaCreacion = DateTime.Now,
                    CarritoSesionId = id,
                    ProductoSeleccionado = obj
                    
                    };
                    _context.CarritoSesionDetalle.Add(detalleSesion);
                }

               var value_ = await _context.SaveChangesAsync();

                if (value_ > 0) {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el detalle del carrito de compras");

            }
        }
    }
}
