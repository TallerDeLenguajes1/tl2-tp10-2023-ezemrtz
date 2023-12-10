using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using tl2_tp10_2023_ezemrtz.Models;
using tl2_tp10_2023_ezemrtz.Repositorios;
using tl2_tp10_2023_ezemrtz.ViewModels;


namespace tl2_tp10_2023_ezemrtz.Controllers;


public class TareaController : Controller
{
    private TareaRepository tareaRepository;
    private TableroRepository tableroRepository;
    private UsuarioRepository usuarioRepository;
    private readonly ILogger<TareaController> _logger;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        tareaRepository = new TareaRepository();
        tableroRepository = new TableroRepository();
        usuarioRepository = new UsuarioRepository();
    }

    public IActionResult Index(int id){
        if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if(!esAdmin()){
            var idUser = (int)HttpContext.Session.GetInt32("id");
            var tableros = tableroRepository.GetByUser(idUser);
            if(tableros.Any(t => t.Id == id)){
                return View(new ListarTareasViewModel(tareaRepository.GetByTablero(id)));
            }
        }else{
            return View(new ListarTareasViewModel(tareaRepository.GetByTablero(id)));
        }
        return RedirectToRoute(new {controller = "Tablero", action = "Index"});
    }

    [HttpGet]
    public IActionResult CreateTarea(int id){
        if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
        var tarea = new CrearTareaViewModel{
            IdTablero = id,
            Usuarios = usuarioRepository.GetAll()
        };
        return View(tarea);
    }

    [HttpPost]
    public IActionResult CreateTarea(CrearTareaViewModel tarea){
        if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
        tareaRepository.Create(tarea.IdTablero, new Tarea(tarea));
        return RedirectToAction("Index", new{id = tarea.IdTablero});
    }

    [HttpGet]
    public IActionResult UpdateTarea(int id){
        if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
        return View(new ModificarTareaViewModel(tareaRepository.Get(id), usuarioRepository.GetAll()));
    }
    [HttpPost]
    public IActionResult UpdateTarea(ModificarTareaViewModel tarea){
        if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
        tareaRepository.Update(tarea.Id,new Tarea(tarea));
        return RedirectToAction("Index", new{id = tarea.IdTablero});
    }

    public IActionResult DeleteTarea(int id){
        if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
        var idTablero = tareaRepository.Get(id).IdTablero;
        tareaRepository.Remove(id);
        return RedirectToAction("Index", new {id = idTablero});
    }

     private bool logueado(){
        return HttpContext.Session.Keys.Any();
    }

    private bool esAdmin(){
        return HttpContext.Session.Keys.Any() && HttpContext.Session.GetString("rol") == NivelAcceso.administrador.ToString();
    }
}