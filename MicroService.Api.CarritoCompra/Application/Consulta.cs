using MediatR;
using MicroService.Api.CarritoCompra.Persistence;
using MicroService.Api.CarritoCompra.RemoteInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicroService.Api.CarritoCompra.Application
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoDTO>
        { 
        public int CarritoSesionId { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDTO>
        {
            private readonly ContextoCarrito _context;
            private readonly ILibrosService _libroService;

            public Manejador(ContextoCarrito context, ILibrosService librosService)
            {
                _context = context;
                _libroService = librosService;
            }

            public async Task<CarritoDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var listaCarrito = new List<CarritoDetalleDTO>();
                var carritoSesion =  await _context.CarritoSesion.FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSesionId);
                var carritoSesionDetalle = await _context.CarritoSesionDetalle.Where(x => x.CarritoSesionId == request.CarritoSesionId).ToListAsync();
                foreach (var libro in carritoSesionDetalle)
                {
                   var response = await _libroService.GetLibro(new Guid(libro.ProductoSeleccionado));
                    if(response.resultado)
                    {
                        var objetoLibro = response.Libro;
                        var carritoDetalle = new CarritoDetalleDTO { 
                        TituloLibro = objetoLibro.Titulo,
                        FechaPublicacion = objetoLibro.FechaPublicacion,
                        LibroId = objetoLibro.LibreriaMaterialId
                        };
                        listaCarrito.Add(carritoDetalle);
                    }
                }

                var carritoSesionDTO = new CarritoDTO { 
                CarritoId = carritoSesion.CarritoSesionId,
                FechaCreacionSesion = carritoSesion.FechaCreacion,
                ListaProductos = listaCarrito
                };

                return carritoSesionDTO;

            }
        }
    }
}
