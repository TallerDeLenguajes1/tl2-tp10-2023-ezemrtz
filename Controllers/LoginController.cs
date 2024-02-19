using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_ezemrtz.Models;
using tl2_tp10_2023_ezemrtz.Repositorios;
using tl2_tp10_2023_ezemrtz.ViewModels;

namespace tl2_tp10_2023_ezemrtz.Controllers;

public class LoginController : Controller{
    private readonly ILogger<LoginController> _logger;
        private IUsuarioRepository _usuarioRepository;

        public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
 
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
                var user = _usuarioRepository.GetByNamePassword(usuarioLogueado.Nombre, usuarioLogueado.Contrasenia);
                LoguearUsuario(user);
                _logger.LogInformation("El usuario {0} ingreso correctamente", user.NombreDeUsuario);
                return RedirectToRoute(new{controller = "Home", action = "Index"});
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                _logger.LogWarning("Intento de acceso invalido - Usuario: {0} Clave ingresada: {1}", usuarioLogueado.Nombre, usuarioLogueado.Contrasenia);
                return RedirectToAction("Index");
            }
        }

        private void LoguearUsuario(Usuario usuario){
            HttpContext.Session.SetInt32("id", usuario.Id);
            HttpContext.Session.SetString("usuario", usuario.NombreDeUsuario);
            HttpContext.Session.SetString("rol", usuario.Rol.ToString());
        }

        public IActionResult DesloguearUsuario(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel());
        }
}