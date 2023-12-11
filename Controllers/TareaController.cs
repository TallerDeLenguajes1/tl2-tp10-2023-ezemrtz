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
                var idUser = (int)HttpContext.Session.GetInt32("id");
                var tableros = _tableroRepository.GetByUser(idUser);
                if(tableros.Any(t => t.Id == id)){
                    return View(new ListarTareasViewModel(_tareaRepository.GetByTablero(id)));
                }
            }else{
                return View(new ListarTareasViewModel(_tareaRepository.GetByTablero(id)));
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
            return View(new ModificarTareaViewModel(_tareaRepository.Get(id), _usuarioRepository.GetAll()));
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

    public IActionResult DeleteTarea(int id){
        try
        {
            if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
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

     private bool logueado(){
        return HttpContext.Session.Keys.Any();
    }

    private bool esAdmin(){
        return HttpContext.Session.Keys.Any() && HttpContext.Session.GetString("rol") == NivelAcceso.administrador.ToString();
    }
}