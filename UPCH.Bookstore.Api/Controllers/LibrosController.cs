using MediatR;
using Microsoft.AspNetCore.Mvc;
using UPCH.Bookstore.Application.Common.Models;
using UPCH.Bookstore.Application.Libros.Commands.CreateLibro;
using UPCH.Bookstore.Application.Libros.Commands.UpdateLibro;
using UPCH.Bookstore.Application.Libros.Commands.DeleteLibro;
using UPCH.Bookstore.Application.Libros.DTOs;
using UPCH.Bookstore.Application.Libros.Queries.GetAllLibros;
using UPCH.Bookstore.Application.Libros.Queries.GetLibroById;

namespace UPCH.Bookstore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LibrosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/libros
        [HttpGet]
        public async Task<ActionResult<Result<List<LibroDto>>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllLibrosQuery());
            if (!result.IsSuccess)
                return Problem(detail: string.Join("; ", result.Errors ?? Array.Empty<string>()), statusCode: 500);

            return Ok(result);
        }

        // GET: api/libros/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Result<LibroDto>>> GetById(int id)
        {
            var result = await _mediator.Send(new GetLibroByIdQuery { Id = id });
            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        // POST: api/libros
        [HttpPost]
        public async Task<ActionResult<Result<int>>> Create([FromBody] CreateLibroCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // PUT: api/libros/{id}
        [HttpPut]
        public async Task<ActionResult<Result>> Update([FromBody] UpdateLibroCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // DELETE: api/libros/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            var command = new DeleteLibroCommand { Id = id };
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
    }
}
