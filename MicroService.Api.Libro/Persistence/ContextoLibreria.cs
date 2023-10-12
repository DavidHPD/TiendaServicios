using MicroService.Api.Libro.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroService.Api.Libro.Persistence
{
    public class ContextoLibreria : DbContext
    {

        public ContextoLibreria() { }

        public ContextoLibreria(DbContextOptions<ContextoLibreria> options) : base(options) { }

        public virtual DbSet<LibreriaMaterial> libreriaMaterial { get; set; }
    }
}
