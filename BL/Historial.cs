using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Historial
    {
        public static ML.Result Add(ML.Historial historial)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenSuperDigitoContext context = new DL.ExamenSuperDigitoContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AddHistorial {historial.Usuario.IdUsuario},{historial.Numero},{historial.Resultado}");

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
        public static ML.Result GetByNumeroHistorial(int numero)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenSuperDigitoContext context = new DL.ExamenSuperDigitoContext())
                {
                    var query = context.Historials.FromSqlRaw($"GetByNumeroHistorial {numero}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Historial historial = new ML.Historial();
                        historial.IdHistorial = query.IdHistorial;
                        historial.Numero = query.Numero;
                        historial.Resultado = query.Resultado;
                        historial.FechaHora = query.FechaHora.Value.ToString("dd-MM-yyyy hh:mm:ss");
                        historial.Usuario = new ML.Usuario();
                        historial.Usuario.IdUsuario = query.IdUsuario.Value;
                        result.Object = historial;
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

        public static ML.Result GetAllHistorial(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenSuperDigitoContext context = new DL.ExamenSuperDigitoContext())
                {
                    var obj = context.Historials.FromSqlRaw($"GetAllHistorial {IdUsuario}").ToList();

                    result.Objects = new List<object>();
                    
                        foreach (var query in obj)
                        {
                            ML.Historial historial = new ML.Historial();
                            historial.IdHistorial = query.IdHistorial;
                            historial.Numero = query.Numero;
                            historial.Resultado = query.Resultado;
                            historial.FechaHora = query.FechaHora.Value.ToString("dd-MM-yyyy hh:mm:ss");
                            historial.Usuario = new ML.Usuario();
                            historial.Usuario.IdUsuario = query.IdUsuario.Value;
                            result.Object = historial;
                            result.Correct = true;

                            result.Objects.Add(historial);
                            result.Correct = true;
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

        public static ML.Result Delete(int idUsuario)
        {
            ML.Historial historia = new ML.Historial();
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenSuperDigitoContext context = new DL.ExamenSuperDigitoContext())
                {
                    int RowsAffected = context.Database.ExecuteSqlRaw($"DeleteHistorial {idUsuario}");

                    if (RowsAffected > 0)
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

        public static ML.Result Update(ML.Historial historial)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenSuperDigitoContext context = new DL.ExamenSuperDigitoContext())
                {
                    int RowsAffected = context.Database.ExecuteSqlRaw($"UpdateHistorial {historial.Numero}, {historial.Usuario.IdUsuario}");

                    if (RowsAffected > 0)
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
    }
}
