using Ejemplo4.Dominio;
using Ejemplo4.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ejemplo4.Aplicacion.Cursos
{
    public class Consulta
    {
        public class ListaCursos : IRequest<List<Curso>> { }

        public class Manejador : IRequestHandler<ListaCursos, List<Curso>>
        {
            private readonly CursosOnlineContext _context;

            public Manejador(CursosOnlineContext context)
            {
                _context = context;
            }

            public async Task<List<Curso>> Handle(ListaCursos request, CancellationToken cancellationToken)
            {
                var resultado = await _context.Curso
                //Con Include se incluye a la entidad Comentario y se obtiene su información
                .Include(x => x.ComentarioLista)
                //Con Include se incluye a la entidad Precio y se obtiene su información
                .Include(x => x.PrecioPromocion)
                //Con Include y ThenInclude se hace el enlace con la entidad Intructor y se obtiene su información
                .Include(x => x.InstructoresLink).ThenInclude(x => x.Instructor).ToListAsync();
                return resultado;
            }
        }
    }
}
