using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class MateriaController1 : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {

            ML.Materia materia = new ML.Materia();
            materia.MateriaList = new List<ML.Materia>();


            var result = BL.Materia.GetAllMateria();
            if (result.Item1)
            {
                materia.MateriaList = result.Item3.MateriaList;
            }
            return View(materia);
        }

        [HttpGet]
        public IActionResult Form(int? idMateria)
        {
            //instanciar
            ML.Materia materia = new ML.Materia();

            List<ML.Materia> listaMateria = new List<ML.Materia>();
            if (idMateria != null)
            {
                var result = BL.Materia.GetByIdMateria(idMateria.Value);
                materia = result.Item3;

                var result2 = BL.Materia.GetAllMateria();
                materia.MateriaList = new List<ML.Materia>();
                listaMateria = result2.Item3.MateriaList;
                materia.MateriaList = listaMateria;
                materia.MateriaList = result2.Item3.MateriaList;

                return View(materia);
            }
            else
            {
                var result3 = BL.Materia.GetAllMateria();
                materia.MateriaList = new List<ML.Materia>();
                listaMateria = result3.Item3.MateriaList;
                materia.MateriaList = listaMateria;

                return View(materia);
            }
        }

        [HttpPost]
        public IActionResult Form(ML.Materia materia)
        {
            //si es diferente de nulo el registro que enviare a editar que se cumpla la condicion para editarlo
            if (materia.IdMateria != 0)
            {
                //  variable donde se guardata el registro, luego llamar a mi gunvion editar de mi modelo y madarle mi parametro
                var result = BL.Materia.UpdateMateria(materia);
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
                var result = BL.Materia.AddMateria(materia);
                if (result.Item1)
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

        public IActionResult Delete(int? IdMateria)
        {
            var result = BL.Materia.DeleteMateria(IdMateria.Value);
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
