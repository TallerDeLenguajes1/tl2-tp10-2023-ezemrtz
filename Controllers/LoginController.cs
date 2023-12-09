using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_ezemrtz.Models;
using tl2_tp10_2023_ezemrtz.Repositorios;
using tl2_tp10_2023_ezemrtz.ViewModels;

namespace tl2_tp10_2023_ezemrtz.Controllers;

public class LoginController : Controller{
    private readonly ILogger<LoginController> _logger;
        private IUsuarioRepository usuarioRepository;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
            usuarioRepository = new UsuarioRepository();
 
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost] // AQUI VIENE EL LOGIN DEL FORM
        public IActionResult Login(LoginViewModel usuarioLogueado) //El control este no deberia estar aca? en api haciamos los controles en otro lado
        {
            var user = usuarioRepository.GetAll().FirstOrDefault(u => u.NombreDeUsuario == usuarioLogueado.Nombre && u.Contrasenia == usuarioLogueado.Contrasenia);
            if(user == null) return RedirectToAction("Index");
            LoguearUsuario(user);

            return RedirectToRoute(new{controller = "Home", action = "Index"});

        }

        private void LoguearUsuario(Usuario usuario){
            HttpContext.Session.SetInt32("id", usuario.Id);
            HttpContext.Session.SetString("usuario", usuario.NombreDeUsuario);
            HttpContext.Session.SetString("rol", usuario.Rol.ToString());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
}