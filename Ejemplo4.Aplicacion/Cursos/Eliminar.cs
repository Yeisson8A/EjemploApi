using Ejemplo4.Persistencia;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ejemplo4.Aplicacion.Cursos
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            [Required(ErrorMessage = "El id es obligatorio")]
            public Guid Id { get; set; }
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
                //Obtener la información del curso según Id
                var curso = _context.Curso.Where(x => x.CursoId == request.Id).FirstOrDefault();

                //Validar si se obtuvo información
                if (curso == null)
                {
                    throw new Exception("No se encontró el curso");
                }

                //Remover el objeto curso del contexto
                _context.Remove(curso);
                //Confirmar cambio en la base de datos
                var resultado = await _context.SaveChangesAsync();

                if(resultado > 0)
                {
                    return Unit.Value;
                }

                //Indicar error
                throw new Exception("No se pudo eliminar el curso");
            }
        }
    }
}
