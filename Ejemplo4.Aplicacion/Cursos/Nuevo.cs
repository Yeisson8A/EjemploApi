using Ejemplo4.Dominio;
using Ejemplo4.Persistencia;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ejemplo4.Aplicacion.Cursos
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            [Required(ErrorMessage = "El campo titulo es obligatorio")]
            public string Titulo { get; set; }
            [Required(ErrorMessage = "El campo descripción es obligatorio")]
            public string Descripcion { get; set; }
            [Required(ErrorMessage = "El campo fecha publicación es obligatorio")]
            //Con ? en el tipo DateTime se indica que el campo acepta nulos
            public DateTime? FechaPublicacion { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CursosOnlineContext _context;

            public Manejador(CursosOnlineContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Objeto curso a ingresar a la base de datos
                var curso = new Curso()
                {
                    CursoId = Guid.NewGuid(),
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion
                };

                //Agregar objeto al contexto de la base de datos
                _context.Curso.Add(curso);

                //Obtener resultado de la operación
                var resultado = await _context.SaveChangesAsync();

                if (resultado > 0)
                {
                    return Unit.Value;
                }

                //Indicar error
                throw new Exception("Ocurrió un error al guardar el registro");
            }
        }
    }
}
