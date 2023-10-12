using AutoMapper;
using GenFu;
using MicroService.Api.Libro.Application;
using MicroService.Api.Libro.Models;
using MicroService.Api.Libro.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
namespace MicroService.Api.Libro.Tests
{
    public class LibrosServiceTest
    {
        private IEnumerable<LibreriaMaterial> ObtenerDataPrueba()
        {
            A.Configure<LibreriaMaterial>()
                   .Fill(x => x.Titulo).AsArticleTitle()
                   .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<LibreriaMaterial>(30);
            lista[0].LibreriaMaterialId = Guid.Empty;
            return lista;
        }

        private Mock<ContextoLibreria> crearContexto()
        {

            var dataTest = ObtenerDataPrueba().AsQueryable();

            var dbSet = new Mock<DbSet<LibreriaMaterial>>();
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataTest.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataTest.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataTest.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataTest.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<LibreriaMaterial>(dataTest.GetEnumerator()));


            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(dataTest.Provider));

            var contexto = new Mock<ContextoLibreria>();
            contexto.Setup(x => x.libreriaMaterial).Returns(dbSet.Object);
            return contexto;

        }


        [Fact]
        public async void GetLibroId()
        {
            
            var mockContexto = crearContexto();
            var mapConfig = new MapperConfiguration(cfg => {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();

            var request = new ConsultaFiltro.Ejecuta();
            request.LibreriaMaterialId = Guid.Empty;

            var manejador = new ConsultaFiltro.Manejador(mockContexto.Object, mapper);
            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());
            Assert.NotNull(libro);
            Assert.True(libro.LibreriaMaterialId == Guid.Empty);
         }




            [Fact]
        public async void GetLibros()
        {
           
            var mockContexto = crearContexto();
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();

            Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object, mapper);

            Consulta.Ejecuta request = new Consulta.Ejecuta();

           var lista = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(lista.Any());
        
        }

        [Fact]
        public async void SaveBook() {


            var options = new DbContextOptionsBuilder<ContextoLibreria>()
                .UseInMemoryDatabase(databaseName: "BaseDatosLibro")
                .Options;
            var _context = new ContextoLibreria(options);
            var request = new Nuevo.Ejecuta();
            request.Titulo = "Libro de Microservice";
            request.AutorLibro = Guid.Empty;
            request.FechaPublicacion = DateTime.Now;

            var manejador = new Nuevo.Manejador(_context);
            var value = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(value != null);

        }

    }

}
