using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AlumnoController1 : Controller
    {
        [HttpGet]
        public IActionResult GetAllAlumnos()
        {

            ML.Alumno alumno = new ML.Alumno();
            alumno.AlumnoList = new List<ML.Alumno>();


            var result = BL.Alumno.GetAllAlumno();
            if (result.Item1)
            {
                alumno.AlumnoList = result.Item3.AlumnoList;
            }
            return View(alumno);
        }


        [HttpGet]
        public IActionResult Form(int?  idAlumno)
        {
            //instanciar
            ML.Alumno alumno = new ML.Alumno();

            List<ML.Alumno> listaAlumnos = new List<ML.Alumno>();
            if(idAlumno != null)
            {
                var result = BL.Alumno.GetByIdAlumno(idAlumno.Value);
                alumno = result.Item3;

                var result2 = BL.Alumno.GetAllAlumno();
                alumno.AlumnoList = new List<ML.Alumno>();
                listaAlumnos = result2.Item3.AlumnoList;
                alumno.AlumnoList = listaAlumnos;
                alumno.AlumnoList = result2.Item3.AlumnoList;

                return View(alumno);
            }
            else
            {
                var result3 = BL.Alumno.GetAllAlumno();
                alumno.AlumnoList = new List<ML.Alumno>();
                listaAlumnos = result3.Item3.AlumnoList;
                alumno.AlumnoList = listaAlumnos;

                return View(alumno);
            }
        }



        [HttpPost]
        public IActionResult Form(ML.Alumno alumno)
        {
            //si es diferente de nulo el registro que enviare a editar que se cumpla la condicion para editarlo
            if(alumno.IdAlumno != 0)
            {
                //  variable donde se guardata el registro, luego llamar a mi gunvion editar de mi modelo y madarle mi parametro
                var result  = BL.Alumno.UpdateAlumno(alumno);
                if (result.Item1)
                {
                    ViewBag.Text = "La actualizacion fue correcta";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Text = "La actualizacion fracaso";
                    return PartialView("Modal");
                }
            }
            else
            {
                var result = BL.Alumno.AddAlumno(alumno);
                if(result.Item1)
                {
                    ViewBag.Text = "Se ha agregado correctamente";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Text = "Ha ocurrido un Error";
                    return PartialView("Modal");
                }
            }
        }

        public IActionResult Delete(int? IdAlumno)
        {
            var result = BL.Alumno.DeleteAlumno(IdAlumno.Value);
            if (result.Item1)
            {
                ViewBag.Text = "Se ha eliminado correctamente";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Text = "Ha ocurrido un Error";
                return PartialView("Modal");
            
            }
        }

    }
}
