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

        [HttpPost]
        public IActionResult Login(LoginViewModel usuarioLogueado) 
        {
            try
            {
                if(!ModelState.IsValid) return RedirectToAction("Index");
                var user = usuarioRepository.GetAll().FirstOrDefault(u => u.NombreDeUsuario == usuarioLogueado.Nombre && u.Contrasenia == usuarioLogueado.Contrasenia);
                if(user == null){
                    _logger.LogWarning("Intento de acceso invalido - Usuario: {0} Clave ingresada: {1}", usuarioLogueado.Nombre, usuarioLogueado.Contrasenia);
                    return RedirectToAction("Index");
                }
                LoguearUsuario(user);
                _logger.LogInformation("El usuario {0} ingreso correctamente", user.NombreDeUsuario);
                return RedirectToRoute(new{controller = "Home", action = "Index"});
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
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