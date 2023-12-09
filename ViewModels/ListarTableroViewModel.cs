using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace tl2_tp10_2023_ezemrtz.ViewModels
{
    public class ListarTablerosViewModel
    {
        public List<Tablero> Tableros {get;set;}
        public ListarTablerosViewModel(List<Tablero> listaTableros){
            Tableros = listaTableros;
        }
    }
}