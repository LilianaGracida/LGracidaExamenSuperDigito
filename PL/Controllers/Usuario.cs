using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace PL.Controllers
{
    public class Usuario : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            ML.Usuario usuario = new ML.Usuario();
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Login(ML.Usuario usuario, string password1)
        {
            // Crear una instancia del algoritmo de hash bcrypt
            var bcrypt = new Rfc2898DeriveBytes(password1, new byte[0], 10000, HashAlgorithmName.SHA256);
            // Obtener el hash resultante para la contraseña ingresada 
            var passwordHash = bcrypt.GetBytes(20);

            if (usuario.Nombre != null)
            {
                // Insertar usuario en la base de datos
                usuario.Password = passwordHash;
                ML.Result result = BL.Usuario.Add(usuario);
                return View();
            }
            else
            {
                string userName = usuario.UserName;
                // Proceso de login
                ML.Result result = BL.Usuario.GetByUserName(userName);
                usuario = (ML.Usuario)result.Object;

                if (usuario.Password.SequenceEqual(passwordHash))
                {
                   // HttpContext.Session.SetString("Usuario", usuario.UserName);
                    return RedirectToAction("GetAll", "Historial",new {usuario.IdUsuario});
                }
            }
            return View();
        }
    }
}
