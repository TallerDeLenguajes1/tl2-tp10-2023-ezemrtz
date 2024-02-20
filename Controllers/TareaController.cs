using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using tl2_tp10_2023_ezemrtz.Models;
using tl2_tp10_2023_ezemrtz.Repositorios;
using tl2_tp10_2023_ezemrtz.ViewModels;


namespace tl2_tp10_2023_ezemrtz.Controllers;


public class TareaController : Controller
{
    private readonly ITareaRepository _tareaRepository;
    private readonly ITableroRepository _tableroRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILogger<TareaController> _logger;

    public TareaController(ILogger<TareaController> logger, ITableroRepository tableroRepository, ITareaRepository tareaRepository, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _tareaRepository = tareaRepository;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index(int id){
        try
        {
            if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
            if(!esAdmin()){
                var idUser = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
                var tableros = _tableroRepository.GetByUser(idUser);
                var tareas = _tareaRepository.GetByTablero(id);
                if(tableros.Any(t => t.Id == id) || tareas.Any(t => t.IdUsuarioAsignado == idUser)){
                    return View(new ListarTareasViewModel(tareas, _tableroRepository.GetAll(), _usuarioRepository.GetAll()));
                }
            }else{

                return View(new ListarTareasViewModel(_tareaRepository.GetByTablero(id), _tableroRepository.GetAll(), _usuarioRepository.GetAll()));
            }
            return RedirectToRoute(new {controller = "Tablero", action = "Index"});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult CreateTarea(int id){
        try
        {
            if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
            var tablero = _tableroRepository.Get(id);
            if(tablero == null || (!esAdmin() && tablero.IdUsuarioPropietario != Convert.ToInt32(HttpContext.Session.GetInt32("id")))) return RedirectToAction("Error");
            var tarea = new CrearTareaViewModel{
                IdTablero = id,
                Usuarios = _usuarioRepository.GetAll()
            };
            return View(tarea);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult CreateTarea(CrearTareaViewModel tarea){
        try
        {
            if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
            if(!ModelState.IsValid) return RedirectToAction("Index", new {id = tarea.IdTablero});
            _tareaRepository.Create(tarea.IdTablero, new Tarea(tarea));
            return RedirectToAction("Index", new{id = tarea.IdTablero});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult UpdateTarea(int id){
        try
        {
            if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
            var tarea = _tareaRepository.Get(id);
            if(tarea != null){
                var tablero = _tableroRepository.Get(tarea.IdTablero);
                if(tablero != null && (esAdmin() || tablero.IdUsuarioPropietario == Convert.ToInt32(HttpContext.Session.GetInt32("id")))){
                    return View(new ModificarTareaViewModel(tarea, _usuarioRepository.GetAll(), tablero.IdUsuarioPropietario));
                }
            }
            return RedirectToAction("Error");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpPost]
    public IActionResult UpdateTarea(ModificarTareaViewModel tarea){
        try
        {
            if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
            if(!ModelState.IsValid) return RedirectToAction("Index", new {id = tarea.IdTablero});
            _tareaRepository.Update(tarea.Id,new Tarea(tarea));
            return RedirectToAction("Index", new{id = tarea.IdTablero});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult UpdateEstado(int id){
        try
        {
            if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
            var tarea = _tareaRepository.Get(id);
            if(tarea != null && (esAdmin() || tarea.IdUsuarioAsignado == Convert.ToInt32(HttpContext.Session.GetInt32("id")) || _tareaRepository.GetIdOwner(id) == Convert.ToInt32(HttpContext.Session.GetInt32("id")))){
                return View(new ModificarEstadoTareaViewModel(tarea));
            }else{
                return RedirectToAction("Error");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpPost]
    public IActionResult UpdateEstado(ModificarEstadoTareaViewModel tarea){
        try
        {
            if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
            if(!ModelState.IsValid) return RedirectToAction("Index", new {id = tarea.IdTablero});
            var tareaNueva = _tareaRepository.Get(tarea.Id);
            tareaNueva.Estado = tarea.Estado;
            _tareaRepository.Update(tarea.Id, tareaNueva);
            return RedirectToAction("Index", new{id = tarea.IdTablero});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    public IActionResult DeleteTarea(int id){
        try
        {
            if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
            if(!esAdmin() && _tareaRepository.GetIdOwner(id) != Convert.ToInt32(HttpContext.Session.GetInt32("id"))) return RedirectToAction("Error");
            if(!ModelState.IsValid) return RedirectToAction("Index", id);
            var idTablero = _tareaRepository.Get(id).IdTablero;
            _tareaRepository.Remove(id);
            return RedirectToAction("Index", new {id = idTablero});
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