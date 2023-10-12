using Microsoft.EntityFrameworkCore;
using MicroService.Api.Autor.Models;

namespace MicroService.Api.Autor.Persistence
{
    public class ContextAutor : DbContext
    {

        public ContextAutor(DbContextOptions<ContextAutor> options) : base(options) { }

        public DbSet<AutorLibro> AutorLibro { get; set; }
        public DbSet<GradoAcademico> GradoAcademico { get; set; }

    }
}
