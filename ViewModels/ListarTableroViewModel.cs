using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace tl2_tp10_2023_ezemrtz.ViewModels
{
    public class ListarTablerosViewModel
    {
        public List<TableroUsuarioViewModel> Tableros {get;set;}
        public ListarTablerosViewModel(List<Tablero> tableros, List<Usuario> usuarios){
            Tableros = new List<TableroUsuarioViewModel>();
            foreach (var t in tableros)
            {
                var user = usuarios.FirstOrDefault(u => u.Id == t.IdUsuarioPropietario);
                var tableroUsuario = new TableroUsuarioViewModel(t, user);
                Tableros.Add(tableroUsuario);
            }
        }
        public ListarTablerosViewModel(List<Tablero> tablerosPropios, List<Tablero> tablerosAsignados, List<Usuario> usuarios, int idUser){
            Tableros = new List<TableroUsuarioViewModel>();
            var userLoguado = usuarios.FirstOrDefault(u => u.Id == idUser);
            foreach (var t in tablerosPropios)
            {
                var tableroUsuario = new TableroUsuarioViewModel(t, userLoguado);
                Tableros.Add(tableroUsuario);
            }
            foreach (var t in tablerosAsignados)
            {
                var user = usuarios.FirstOrDefault(u => u.Id == t.IdUsuarioPropietario);
                var tableroUsuario = new TableroUsuarioViewModel(t, user);
                Tableros.Add(tableroUsuario);
            }
            
        }
    }
}