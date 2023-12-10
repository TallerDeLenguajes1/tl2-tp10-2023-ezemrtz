using tl2_tp10_2023_ezemrtz.ViewModels;

namespace tl2_tp10_2023_ezemrtz;


public enum EstadoTarea{
    Ideas,
    ToDo,
    Doing,
    Review,
    Done
}
public class Tarea{
    private int id;
    private int idTablero;
    private string nombre;
    private string descripcion;
    private string color;
    private EstadoTarea estado;
    private int? idUsuarioAsignado;

    public int Id { get => id; set => id = value; }
    public int IdTablero { get => idTablero; set => idTablero = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public string Color { get => color; set => color = value; }
    public EstadoTarea Estado { get => estado; set => estado = value; }
    public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }

    public Tarea(){}
    public Tarea(CrearTareaViewModel tarea){
        this.IdTablero = tarea.IdTablero;
        this.Nombre = tarea.Nombre;
        this.Descripcion = tarea.Descripcion;
        this.Color = tarea.Color;
        this.Estado = tarea.Estado;
        this.IdUsuarioAsignado = tarea.IdUsuarioAsignado;
    }
    public Tarea(ModificarTareaViewModel tarea){
        this.Id = tarea.Id;
        this.IdTablero = tarea.IdTablero;
        this.Nombre = tarea.Nombre;
        this.Descripcion = tarea.Descripcion;
        this.Color = tarea.Color;
        this.Estado = tarea.Estado;
        this.IdUsuarioAsignado = tarea.IdUsuarioAsignado;
    }

}