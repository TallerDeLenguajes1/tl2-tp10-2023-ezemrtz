using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_ezemrtz.Models;
using tl2_tp10_2023_ezemrtz.Repositorios;


namespace tl2_tp10_2023_ezemrtz.Controllers;
[ApiController]
[Route("[controller]")]
public class UsuarioController : Controller
{
    private UsuarioRepository usuarioRepository;
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        usuarioRepository = new UsuarioRepository();
    }

    [HttpPost("api/CrearUsuario")]
    public IActionResult CreateUser(Usuario usuario){
        usuarioRepository.Create(usuario);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult GetAllUsers(){
        var usuarios = usuarioRepository.GetAll();
        return View(usuarios);
    }
    [HttpPut("api/ModificarUsuario")]
    public IActionResult UpdateUser(int idUser, Usuario usuario){
        usuarioRepository.Update(idUser, usuario);
        var usuarioModificado = usuarioRepository.Get(idUser);
        return View(usuarioModificado);
    }
    [HttpDelete("api/EliminarUsuario")]
    public IActionResult DeleteUser(int idUser){
        usuarioRepository.Remove(idUser);
        return RedirectToAction("Index");
    }

}