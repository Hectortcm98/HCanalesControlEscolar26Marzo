using System;
using System.Collections.Generic;

namespace DL;

public partial class Alumno
{
    public int IdAlumno { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public virtual ICollection<AlumnoMaterium> AlumnoMateria { get; set; } = new List<AlumnoMaterium>();
}
