using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_ezemrtz.Models;
using tl2_tp10_2023_ezemrtz.Repositorios;
using tl2_tp10_2023_ezemrtz.ViewModels;


namespace tl2_tp10_2023_ezemrtz.Controllers;

public class UsuarioController : Controller
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index(){
        if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if(esAdmin()){
            var usuarios = _usuarioRepository.GetAll();
            return View(new ListarUsuariosViewModel(usuarios));
        }else{
            var usuarios = new List<Usuario>();
            usuarios.Add(_usuarioRepository.Get((int)HttpContext.Session.GetInt32("id")));
            return View(new ListarUsuariosViewModel(usuarios));
        }
    }

    [HttpGet]
    public IActionResult CreateUser(){
        return View(new CrearUsuarioViewModel());
    }

    [HttpPost]
    public IActionResult CreateUser(CrearUsuarioViewModel usuario){
        if(!ModelState.IsValid) return RedirectToAction("CreateUser");
        _usuarioRepository.Create(new Usuario(usuario));
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult UpdateUser(int idUser){
        if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
        var user = _usuarioRepository.Get(idUser);
        return View(new ModificarUsuarioViewModel(user));
    }
    [HttpPost]
    public IActionResult UpdateUser(ModificarUsuarioViewModel usuario){
        if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if(!ModelState.IsValid) return RedirectToAction("Index");
        _usuarioRepository.Update(usuario.Id,new Usuario(usuario));
        return RedirectToAction("Index");
    }

    public IActionResult DeleteUser(int idUser){
        if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if(!ModelState.IsValid) return RedirectToAction("Index");
        _usuarioRepository.Remove(idUser);
        return RedirectToAction("Index");
    }

    private bool logueado(){
        return HttpContext.Session.Keys.Any();
    }

    private bool esAdmin(){
        return HttpContext.Session.Keys.Any() && HttpContext.Session.GetString("rol") == NivelAcceso.administrador.ToString();
    }

}