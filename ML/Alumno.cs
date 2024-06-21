using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Alumno
    {

        public int IdAlumno { get; set; }

        public string Nombre { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public List<ML.Alumno> AlumnoList { get; set;}

        public ML.AlumnoMateria AlumnoMateria { get; set; }  
    }
}
