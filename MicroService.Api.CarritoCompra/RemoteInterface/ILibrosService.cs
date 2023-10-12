using MicroService.Api.CarritoCompra.RemoteModel;
using System;
using System.Threading.Tasks;

namespace MicroService.Api.CarritoCompra.RemoteInterface
{
    public interface ILibrosService
    {

        Task<(bool resultado, LibroRemote Libro, string ErrorMessage)> GetLibro(Guid LibroId);
    }
}
