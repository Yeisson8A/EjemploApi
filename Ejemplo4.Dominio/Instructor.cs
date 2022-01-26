using System;
using System.Collections.Generic;
using System.Text;

namespace Ejemplo4.Dominio
{
    public class Instructor
    {
        //Se indica a las claves primarias que son de tipo Guid
        public Guid InstructorId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Grado { get; set; }
        //Para referencia a nivel de objetos Instructor-CursoInstructor (Muchos a Muchos)
        public ICollection<CursoInstructor> CursoLink { get; set; }
    }
}
