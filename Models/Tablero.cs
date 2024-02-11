

using tl2_tp10_2023_ezemrtz.ViewModels;

namespace tl2_tp10_2023_ezemrtz;

public class Tablero{
    private int id;
    private int idUsuarioPropietario;
    private string nombre;
    private string descripcion;

    public int Id { get => id; set => id = value; }
    public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }

    public Tablero(){}
    public Tablero(CrearTableroViewModel tablero){
        this.IdUsuarioPropietario = tablero.IdUsuarioPropietario;
        this.Nombre = tablero.Nombre;
        this.Descripcion = tablero.Descripcion;
    }
    public Tablero(ModificarTableroViewModel tablero){
        this.Id = tablero.Id;
        this.IdUsuarioPropietario = tablero.IdUsuarioPropietario;
        this.Nombre = tablero.Nombre;
        this.Descripcion = tablero.Descripcion;
    }   
}