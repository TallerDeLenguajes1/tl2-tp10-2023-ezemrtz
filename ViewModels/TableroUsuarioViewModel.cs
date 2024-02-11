using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace tl2_tp10_2023_ezemrtz.ViewModels
{
    public class TableroUsuarioViewModel
    {
        public int Id { get; set; }
        public string NombreUsuarioPropietario { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public TableroUsuarioViewModel(Tablero tablero, Usuario usuario){
           Id = tablero.Id;
           NombreUsuarioPropietario = usuario.NombreDeUsuario;
           Nombre = tablero.Nombre;
           Descripcion = tablero.Descripcion;
        }
        public TableroUsuarioViewModel(){}
    }
}