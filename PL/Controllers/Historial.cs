using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Historial : Controller
    {
        public IActionResult GetAll(ML.Usuario  usuario)
        {
            int idUsuario = usuario.IdUsuario;
            ML.Result result = BL.Historial.GetAllHistorial(idUsuario);
            ML.Historial historial = new ML.Historial();
            if (result.Correct)
            {
                historial.Hitoriales = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta " + result.ErrorMessage;
            }

            return View(historial);

            return View();
        }
    }
}
