using System;
using System.Collections.Generic;

namespace DL;

public partial class AlumnoMaterium
{
    public int? IdAlumno { get; set; }

    public int? IdMateria { get; set; }

    public int IdalumnoMateria { get; set; }

    public virtual Alumno? IdAlumnoNavigation { get; set; }

    public virtual Materia? IdMateriaNavigation { get; set; }
}
