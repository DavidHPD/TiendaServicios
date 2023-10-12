using MicroService.Api.CarritoCompra.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroService.Api.CarritoCompra.Persistence
{
    public class ContextoCarrito : DbContext
    {
        public ContextoCarrito(DbContextOptions<ContextoCarrito> options) : base(options) { }

        public DbSet<CarritoSesion> CarritoSesion { get; set; }
        public DbSet<CarritoSesionDetalle> CarritoSesionDetalle { get; set; }
    }
}
