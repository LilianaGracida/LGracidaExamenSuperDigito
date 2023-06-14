using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenSuperDigitoContext context = new DL.ExamenSuperDigitoContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AddUsuario '{usuario.Nombre}','{usuario.UserName}',@Password", new SqlParameter("@Password", usuario.Password));

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result GetByUserName(string userName)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenSuperDigitoContext context = new DL.ExamenSuperDigitoContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"GetByUserName {userName}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.UserName = query.UserName;
                        usuario.Password = query.Password;
                        result.Object = usuario;
                        result.Correct = true;
                    }

                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}