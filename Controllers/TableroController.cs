using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_ezemrtz.Models;
using tl2_tp10_2023_ezemrtz.Repositorios;
using tl2_tp10_2023_ezemrtz.ViewModels;

namespace tl2_tp10_2023_ezemrtz.Controllers;


public class TableroController : Controller
{
    private readonly ITableroRepository _tableroRepository;
    private readonly ITareaRepository _tareaRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILogger<TableroController> _logger;

    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository, ITareaRepository tareaRepository, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
        _tareaRepository = tareaRepository;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index(){
        try
        {
            if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
            if(esAdmin()){
                var tableros = _tableroRepository.GetAll();
                var usuarios = _usuarioRepository.GetAll();
                var idUserLogueado = Convert.ToInt32(HttpContext.Session.GetInt32("id"));

                return View(new ListarTablerosViewModel(_tableroRepository.GetAll(),_tableroRepository.GetByUser(idUserLogueado), _tableroRepository.GetByAssignedTask(idUserLogueado), _usuarioRepository.GetAll(), idUserLogueado));
            }else{
                var idUserLogueado = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
                return View(new ListarTablerosViewModel(_tableroRepository.GetByUser(idUserLogueado), _tableroRepository.GetByAssignedTask(idUserLogueado), _usuarioRepository.GetAll(), idUserLogueado));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult CreateTablero(){
        try
        {
            if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
            return View(new CrearTableroViewModel{IdUsuarioPropietario = Convert.ToInt32(HttpContext.Session.GetInt32("id"))});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult CreateTablero(CrearTableroViewModel tablero){
        try
        {
            if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
            if(!ModelState.IsValid) return RedirectToAction("Index");

            _tableroRepository.Create(new Tablero(tablero));
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult UpdateTablero(int id){
        try
        {
            if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
            var tablero = _tableroRepository.Get(id);
            if(tablero == null || (!esAdmin() && tablero.IdUsuarioPropietario != Convert.ToInt32(HttpContext.Session.GetInt32("id")))) return RedirectToAction("Error");
            return View(new ModificarTableroViewModel(_tableroRepository.Get(id)));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpPost]
    public IActionResult UpdateTablero(ModificarTableroViewModel tablero){
        try
        {
            if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
            if(!ModelState.IsValid) return RedirectToAction("Index");
            _tableroRepository.Update(tablero.Id, new Tablero(tablero));
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    public IActionResult DeleteTablero(int id){
        try
        {
            if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
            if(!esAdmin()){
                var tablero = _tableroRepository.Get(id);
                if(Convert.ToInt32(HttpContext.Session.GetInt32("id")) != tablero.IdUsuarioPropietario) return RedirectToAction("Error");
            }
            var tareas = _tareaRepository.GetByTablero(id);
            foreach (var tarea in tareas)
            {
                _tareaRepository.Remove(tarea.Id);
            }
            _tableroRepository.Remove(id);
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