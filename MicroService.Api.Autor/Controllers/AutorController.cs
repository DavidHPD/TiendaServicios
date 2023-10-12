using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using MicroService.Api.Autor.Application;
using MicroService.Api.Autor.Models;
using System.Collections.Generic;

namespace MicroService.Api.Autor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AutorController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }


        [HttpGet]
        public async Task<ActionResult<List<AutorDTO>>> getAutores()
        {

            return await _mediator.Send(new Consulta.ListaAutor());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDTO>> GetAutorLibro(string id)
        {

            return await _mediator.Send(new ConsultaFiltro.AutorUnico { AutorGuid = id});
        
        }

    }
}
