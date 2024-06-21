using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Alumno
    {

        public static (bool, string, ML.Alumno, Exception) GetAllAlumno()
        {
			ML.Alumno alumno = new ML.Alumno();
			try
			{
				using(DL.HcanalesControlEscolarContext context = new DL.HcanalesControlEscolarContext())
				{
					var query = (from Alumno in context.Alumnos select new
					{
						IdAlumno = Alumno.IdAlumno,
						Nombre = Alumno.Nombre,
						ApellidoPaterno = Alumno.ApellidoPaterno,
						ApellidoMaterno = Alumno.ApellidoMaterno
					}).ToList();

					if(query != null)
					{
						alumno.AlumnoList = new List<ML.Alumno>();

                        foreach (var item in query )
                        {
							ML.Alumno alumno1 = new ML.Alumno();
							alumno1.IdAlumno = item.IdAlumno;
							alumno1.Nombre = item.Nombre;
							alumno1.ApellidoPaterno= item.ApellidoPaterno;
							alumno1.ApellidoMaterno= item.ApellidoMaterno;
							alumno.AlumnoList.Add( alumno1);

                        }
						return (true, "Exito", alumno, null);
                    }
					
				}
			}
			catch (Exception ex)
			{

				return (false,ex.Message, null, ex);

			}
			return (true, null, null, null);
        }

        public static (bool, string, ML.Alumno, Exception) GetByIdAlumno(int idAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();
            try
            {
                using (DL.HcanalesControlEscolarContext context = new DL.HcanalesControlEscolarContext())
                {
                    var query = (from Alumno in context.Alumnos
                                 where Alumno.IdAlumno == idAlumno
                                 select new
                                 {
                                     Nombre = Alumno.Nombre,
                                     ApellidoPaterno = Alumno.ApellidoPaterno,
                                     ApellidoMaterno = Alumno.ApellidoMaterno
                                 }).SingleOrDefault();

                    if (query != null)
                    {
                        alumno.Nombre = query.Nombre;
                        alumno.ApellidoPaterno = query.ApellidoPaterno;
                        alumno.ApellidoMaterno = query.ApellidoMaterno;

                        return (true, null, alumno, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null, ex);
            }
            return (true, null, null, null);
        }

        public static (bool, string, ML.Alumno, Exception) AddAlumno(ML.Alumno alumno)
        {
            try
            {
                using (DL.HcanalesControlEscolarContext context = new DL.HcanalesControlEscolarContext())
                {
                    // Llama al procedimiento almacenado con los parámetros adecuados
                    var query = context.Database.ExecuteSqlRaw("EXEC AddEscolar @Nombre, @ApellidoPaterno, @ApellidoMaterno",
                        new SqlParameter("@Nombre", alumno.Nombre),
                        new SqlParameter("@ApellidoPaterno", alumno.ApellidoPaterno),
                        new SqlParameter("@ApellidoMaterno", alumno.ApellidoMaterno));

                    if (query > 0)
                    {
                        return (true, null, alumno, null);
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

        public static (bool, string, ML.Alumno, Exception) UpdateAlumno(ML.Alumno alumno)
        {
            try
            {
                using (DL.HcanalesControlEscolarContext context = new DL.HcanalesControlEscolarContext())
                {
                    // Llama al procedimiento almacenado con los parámetros adecuados
                    var query = context.Database.ExecuteSqlRaw("EXEC UpdateEscolar @IdAlumno, @Nombre, @ApellidoPaterno, @ApellidoMaterno",
                        new SqlParameter("@IdAlumno", alumno.IdAlumno),
                        new SqlParameter("@Nombre", alumno.Nombre),
                        new SqlParameter("@ApellidoPaterno", alumno.ApellidoPaterno),
                        new SqlParameter("@ApellidoMaterno", alumno.ApellidoMaterno));

                    if (query > 0)
                    {
                        return (true, null, alumno, null);
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

        public static (bool, string, Exception) DeleteAlumno(int IdAlumno)
        {
            try
            {
                using (DL.HcanalesControlEscolarContext context = new DL.HcanalesControlEscolarContext())
                {
                    // Llama al procedimiento almacenado solo con el parámetro '@IdAlumno'
                    var query = context.Database.ExecuteSqlRaw("EXEC DeleteEscolar @IdAlumno",
                        new SqlParameter("@IdAlumno", IdAlumno));

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
