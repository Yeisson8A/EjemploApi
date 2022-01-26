using Ejemplo4.Aplicacion.Cursos;
using Ejemplo4.Dominio;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ejemplo4.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CursosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Método GET
        [HttpGet]
        public async Task<ActionResult<List<Curso>>> ObtenerCursos()
        {
            return await _mediator.Send(new Consulta.ListaCursos());
        }

        //Método POST
        [HttpPost]
        public async Task<ActionResult<Unit>> RegistrarCurso(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }

        //Método DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> EliminarCurso(Guid id)
        {
            return await _mediator.Send(new Eliminar.Ejecuta() { Id = id });
        }

        //Método PUT
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> ActualizarCurso(Guid id, Editar.Ejecuta data)
        {
            //Asignar id recibido
            data.CursoId = id;
            return await _mediator.Send(data);
        }
    }
}
