using tl2_tp10_2023_ezemrtz.ViewModels;
namespace tl2_tp10_2023_ezemrtz;

public enum NivelAcceso{
    administrador,
    operador
}
public class Usuario{
    private int id;
    private string nombreDeUsuario;
    private string contrasenia;
    private NivelAcceso rol;

    public int Id { get => id; set => id = value; }
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
    public NivelAcceso Rol { get => rol; set => rol = value; }

    public Usuario(){}
    public Usuario(CrearUsuarioViewModel usuario){
        this.NombreDeUsuario = usuario.Nombre;
        this.Contrasenia = usuario.Contrasenia;
        this.Rol = usuario.Rol;
    }
    public Usuario(ModificarUsuarioViewModel usuario){
        this.NombreDeUsuario = usuario.Nombre;
        this.Contrasenia = usuario.Contrasenia;
        this.Rol = usuario.Rol;
    }
}