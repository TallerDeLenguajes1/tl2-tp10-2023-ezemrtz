using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace tl2_tp10_2023_ezemrtz.ViewModels
{
    public class ListarTablerosViewModel
    {
        public List<TableroUsuarioViewModel> TablerosPropios {get;set;}
        public List<TableroUsuarioViewModel> TablerosAjenos {get;set;}
        public List<TableroUsuarioViewModel> Tableros {get;set;}
        public ListarTablerosViewModel(List<Tablero> tablerosPropios, List<Tablero> tablerosAjenos, List<Usuario> usuarios, int idUser){
            Tableros = new List<TableroUsuarioViewModel>();
            TablerosPropios = new List<TableroUsuarioViewModel>();
            TablerosAjenos = new List<TableroUsuarioViewModel>();
            var userLoguado = usuarios.FirstOrDefault(u => u.Id == idUser);
            foreach (var t in tablerosPropios)
            {
                var tableroUsuario = new TableroUsuarioViewModel(t, userLoguado);
                TablerosPropios.Add(tableroUsuario);
            }
            foreach (var t in tablerosAjenos)
            {
                var user = usuarios.FirstOrDefault(u => u.Id == t.IdUsuarioPropietario);
                var tableroUsuario = new TableroUsuarioViewModel(t, user);
                TablerosAjenos.Add(tableroUsuario);
            }
            
        }
        public ListarTablerosViewModel(List<Tablero> todosTableros, List<Tablero> tablerosPropios, List<Tablero> tablerosAjenos, List<Usuario> usuarios, int idUser){
            Tableros = new List<TableroUsuarioViewModel>();
            TablerosPropios = new List<TableroUsuarioViewModel>();
            TablerosAjenos = new List<TableroUsuarioViewModel>();
            var userLoguado = usuarios.FirstOrDefault(u => u.Id == idUser);
            foreach (var t in tablerosPropios)
            {
                var tableroUsuario = new TableroUsuarioViewModel(t, userLoguado);
                TablerosPropios.Add(tableroUsuario);

                var duplicado = todosTableros.FirstOrDefault(j => j.Id == t.Id);
                if(duplicado != null) todosTableros.Remove(duplicado);
            }
            foreach (var t in tablerosAjenos)
            {
                var user = usuarios.FirstOrDefault(u => u.Id == t.IdUsuarioPropietario);
                var tableroUsuario = new TableroUsuarioViewModel(t, user);
                TablerosAjenos.Add(tableroUsuario);

                var duplicado = todosTableros.FirstOrDefault(j => j.Id == t.Id);
                if(duplicado != null) todosTableros.Remove(duplicado);
            }
            foreach (var t in todosTableros)
            {
                var user = usuarios.FirstOrDefault(u => u.Id == t.IdUsuarioPropietario);
                var tableroUsuario = new TableroUsuarioViewModel(t, user);
                Tableros.Add(tableroUsuario);
            }
            
        }
    }
}