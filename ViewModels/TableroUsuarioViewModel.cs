using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace tl2_tp10_2023_ezemrtz.ViewModels
{
    public class TableroUsuarioViewModel
    {
        public int IdTablero { get; set; }
        public int IdUsuarioPropietario { get; set; }
        public string NombreUsuarioPropietario { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        public TableroUsuarioViewModel(Tablero tablero, Usuario usuario){
           IdTablero = tablero.Id;
           IdUsuarioPropietario = usuario.Id;
           NombreUsuarioPropietario = usuario.NombreDeUsuario;
           Nombre = tablero.Nombre;
           Descripcion = tablero.Descripcion;
        }
        public TableroUsuarioViewModel(){}
    }
}