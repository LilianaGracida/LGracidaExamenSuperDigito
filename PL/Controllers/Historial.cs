using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Historial : Controller
    {
        public IActionResult GetAll(ML.Usuario  usuario,ML.Historial historial)
         {
            int idUsuario = usuario.IdUsuario;
            ML.Result result = BL.Historial.GetAllHistorial(idUsuario);
           
            if (result.Correct)
            {
                historial.Hitoriales = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta " + result.ErrorMessage;
            }
            historial.Usuario = new ML.Usuario();
            historial.Usuario.IdUsuario = idUsuario;

            return View(historial);

        }

        [HttpPost]
        public IActionResult Agregar(ML.Historial historial)
        {
            ML.Result resultCalcular = BL.Historial.Calcular(historial);

            historial.Resultado = resultCalcular.Resultado; 

            int numero = historial.Numero;
            ML.Result resultNumero = BL.Historial.GetByNumeroHistorial(numero);
            if (resultNumero.Correct)
            {
                ML.Result resultUpdate = BL.Historial.Update(historial);

                return RedirectToAction("GetAll", "Historial", new {historial.Usuario.IdUsuario,historial.Numero, historial.Resultado });
            }
            else
            {
                ML.Result result = BL.Historial.Add(historial);
                return RedirectToAction("GetAll", "Historial", new { historial.Usuario.IdUsuario,historial.Numero,historial.Resultado });

            }
        }
        [HttpGet]
        public IActionResult Delete(int idUsuario)
        {
            // int idUsuario = historial.Usuario.IdUsuario;
            ML.Result result = BL.Historial.Delete(idUsuario);
            if (result.Correct)
            {
                return RedirectToAction("GetAll", "Historial", new { idUsuario });
            }
            return View();
        }

    }
}
