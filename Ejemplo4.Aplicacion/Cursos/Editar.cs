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
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public Guid CursoId { get; set; }
            [Required(ErrorMessage = "El titulo es obligatorio")]
            public string Titulo { get; set; }
            [Required(ErrorMessage = "La descripción es obligatoria")]
            public string Descripcion { get; set; }
            [Required(ErrorMessage = "La fecha de publicación es obligatoria")]
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
                //Obtener información del curso según Id
                var curso = await _context.Curso.FindAsync(request.CursoId);

                //Validar si se obtuvo información
                if (curso == null)
                {
                    throw new Exception("El curso no existe");
                }

                //Asignar valores a objeto
                curso.Titulo = request.Titulo;
                curso.Descripcion = request.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion;

                //Confirmar cambios en la base de datos
                var resultado = await _context.SaveChangesAsync();

                if (resultado > 0)
                {
                    return Unit.Value;
                }

                //Indicar error
                throw new Exception("No se pudo actualizar el curso");
            }
        }
    }
}
