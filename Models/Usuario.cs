namespace tl2_tp10_2023_ezemrtz;

public enum NivelAcceso{
    administrador = 1,
    operador = 2
}
public class Usuario{
    private int id;
    private string nombreDeUsuario;
    private string contrasenia;
    private NivelAcceso nivelDeAcceso;

    public int Id { get => id; set => id = value; }
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
    public NivelAcceso NivelDeAcceso { get => nivelDeAcceso; set => nivelDeAcceso = value; }
}