using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Materia
    {
        public static (bool, string, ML.Materia, Exception) GetAllMateria()

        {
            ML.Materia materia = new ML.Materia();
            try
            {
                using (DL.HcanalesControlEscolarContext context = new DL.HcanalesControlEscolarContext())
                {
                    var query = (from Materia in context.Materias
                                 select new
                                 {
                                     IdMateria = Materia.IdMateria,
                                     NombreMateria = Materia.Nombre,
                                     Costo = ((decimal)Materia.Costo),
                                     
                                 }).ToList();

                    if (query != null)
                    {
                        materia.MateriaList = new List<ML.Materia>();

                        foreach (var item in query)
                        {
                            ML.Materia materia1 = new ML.Materia();
                            materia1.IdMateria = item.IdMateria;
                            materia1.NombreMateria = item.NombreMateria;
                            materia1.Costo = item.Costo;
                            materia.MateriaList.Add(materia1);

                        }
                        return (true, null, materia, null);
                    }

                }
            }
            catch (Exception ex)
            {

                return (false, "NO hay ningun elemento", null, ex);

            }
            return (true, null, null, null);
        }


        public static (bool, string, ML.Materia, Exception) GetByIdMateria(int idMateria)
        {
            ML.Materia materia = new ML.Materia();
            try
            {
                using (DL.HcanalesControlEscolarContext context = new DL.HcanalesControlEscolarContext())
                {
                    var query = (from Materia in context.Materias
                                 where Materia.IdMateria == idMateria
                                 select new
                                 {
                                     Nombre = Materia.Nombre,
                                     Costo = Materia.Costo,
                                     
                                 }).SingleOrDefault();

                    if (query != null)
                    {
                        materia.NombreMateria = query.Nombre ;
                        materia.Costo = query.Costo;                  

                        return (true, null, materia, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null, ex);
            }
            return (true, null, null, null);
        }


        public static (bool, string, ML.Materia, Exception) AddMateria(ML.Materia materia)
        {
            try
            {
                using (DL.HcanalesControlEscolarContext context = new DL.HcanalesControlEscolarContext())
                {
                    // Llama al procedimiento almacenado con los parámetros adecuados
                    var query = context.Database.ExecuteSqlRaw("EXEC AddMateria @Nombre, @Costo",
                        new SqlParameter("@Nombre", materia.NombreMateria),
                        new SqlParameter("@Costo", materia.Costo));


                    if (query > 0)
                    {
                        return (true, null, materia, null);
                    }
                    else
                    {
                        return (false, "No se agregó correctamente", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "Ocurrió un error al agregar el alumno", null, ex);
            }
        }

        public static (bool, string, ML.Materia, Exception) UpdateMateria(ML.Materia materia)
        {
            try
            {
                using (DL.HcanalesControlEscolarContext context = new DL.HcanalesControlEscolarContext())
                {
                    // Llama al procedimiento almacenado con los parámetros adecuados
                    var query = context.Database.ExecuteSqlRaw("EXEC UpdateMateria @IdMateria, @Nombre, @Costo",
                        new SqlParameter("@IdMateria", materia.IdMateria),
                        new SqlParameter("@Nombre", materia.NombreMateria),
                        new SqlParameter("@Costo", materia.Costo));

                    if (query > 0)
                    {
                        return (true, null, materia, null);
                    }
                    else
                    {
                        return (false, "No se actualizó correctamente", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "Ocurrió un error al actualizar el alumno", null, ex);
            }
        }

        public static (bool, string, Exception) DeleteMateria(int IdMateria)
        {
            try
            {
                using (DL.HcanalesControlEscolarContext context = new DL.HcanalesControlEscolarContext())
                {
                    // Llama al procedimiento almacenado solo con el parámetro '@IdAlumno'
                    var query = context.Database.ExecuteSqlRaw("EXEC DeleteMateria @IdMateria",
                        new SqlParameter("@IdMateria", IdMateria));

                    if (query > 0)
                    {
                        return (true, null, null);
                    }
                    else
                    {
                        return (false, "No se eliminó correctamente", null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "Ocurrió un error al eliminar el alumno", ex);
            }
        }
    }
}
