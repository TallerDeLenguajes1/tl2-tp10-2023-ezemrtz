using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_ezemrtz.Models;
using tl2_tp10_2023_ezemrtz.Repositorios;
using tl2_tp10_2023_ezemrtz.ViewModels;


namespace tl2_tp10_2023_ezemrtz.Controllers;

public class UsuarioController : Controller
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITableroRepository _tableroRepository;
    private readonly ITareaRepository _tareaRepository;
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository, ITableroRepository tableroRepository, ITareaRepository tareaRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
        _tableroRepository = tableroRepository;
        _tareaRepository = tareaRepository;
    }

    public IActionResult Index(){
        try
        {
            if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
            if(esAdmin()){
                var usuarios = _usuarioRepository.GetAll();
                return View(new ListarUsuariosViewModel(_usuarioRepository.Get((int)HttpContext.Session.GetInt32("id")!),usuarios));
            }else{
                var usuarios = new List<Usuario>();
                usuarios.Add(_usuarioRepository.Get(Convert.ToInt32(HttpContext.Session.GetInt32("id"))));
                return View(new ListarUsuariosViewModel(_usuarioRepository.Get((int)HttpContext.Session.GetInt32("id")!),usuarios));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult CreateUser(){
        try
        {
            return View(new CrearUsuarioViewModel());   
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult CreateUser(CrearUsuarioViewModel usuario){
        try
        {
            if(!ModelState.IsValid) return RedirectToAction("CreateUser");
            if(_usuarioRepository.ExistsByName(usuario.Nombre) ){
                ViewBag.Mensaje = "El nombre que ingresó ya está en uso.";
                return View(usuario);
            }
            _usuarioRepository.Create(new Usuario(usuario));
            return RedirectToAction("Index", usuario);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");   
        }
    }

    [HttpGet]
    public IActionResult UpdateUser(int idUser){
        try
        {
            if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
            if(!esAdmin() && idUser != Convert.ToInt32(HttpContext.Session.GetInt32("id"))) return RedirectToAction("Error");
            var user = _usuarioRepository.Get(idUser);
            return View(new ModificarUsuarioViewModel(user));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");   
        }
    }
    [HttpPost]
    public IActionResult UpdateUser(ModificarUsuarioViewModel usuario){
        try
        {
            if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
            if(!ModelState.IsValid) return RedirectToAction("Index");
            if(_usuarioRepository.ExistsByName(usuario.Nombre) ){
                var usuarioByName = _usuarioRepository.GetByName(usuario.Nombre);
                if(usuarioByName.Id != usuario.Id){
                    ViewBag.Mensaje = "El nombre que ingresó ya está en uso.";
                    return View(usuario);
                }
            }
            _usuarioRepository.Update(usuario.Id,new Usuario(usuario));
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");   
        }
    }

    public IActionResult DeleteUser(int idUser){
        try
        {
            if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
            if(!ModelState.IsValid || HttpContext.Session.GetInt32("id") == idUser || !esAdmin()) return RedirectToAction("Error");
            var tableros = _tableroRepository.GetByUser(idUser);
            bool tareaNull = true;
            foreach (var tab in tableros)
            {
                var tareas = _tareaRepository.GetByTablero(tab.Id);
                if(tareas.Count == 0) tareaNull = false;
                foreach (var tar  in tareas)
                {
                    _tareaRepository.Remove(tar.Id);
                }
                _tableroRepository.Remove(tab.Id);
            }
            if(!tareaNull)_tareaRepository.DesasignarByUser(idUser);
            _usuarioRepository.Remove(idUser);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString()); 
            return RedirectToAction("Error");  
        }
    }

    public IActionResult Error(){
        return View(new ErrorViewModel());
    }

    private bool logueado(){
        return HttpContext.Session.Keys.Any();
    }

    private bool esAdmin(){
        return HttpContext.Session.Keys.Any() && HttpContext.Session.GetString("rol") == NivelAcceso.administrador.ToString();
    }

}