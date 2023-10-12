using MediatR;
using MicroService.Api.Libro.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroService.Api.Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibreriaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LibreriaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateBook(Nuevo.Ejecuta data)
        {

            return await _mediator.Send(data);

        }

        [HttpGet]
        public async Task<ActionResult<List<LibreriaDTO>>> GetBooks()
        {

            return await _mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibreriaDTO>> GetBookById(Guid id)
        {

            return await _mediator.Send(new ConsultaFiltro.Ejecuta { LibreriaMaterialId = id });
        }



    }
}
