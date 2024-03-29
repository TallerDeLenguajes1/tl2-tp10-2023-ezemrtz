namespace tl2_tp10_2023_ezemrtz.Repositorios{
    public interface ITareaRepository{
        public void Create(int idTablero, Tarea tarea);
        public void Update(int id, Tarea tarea);
        public Tarea Get(int id);
        public List<Tarea> GetAll();
        public int GetIdOwner(int id);
        public List<Tarea> GetByTablero(int idTablero);
        public List<Tarea> GetByUser(int idUsuario);
        public void DesasignarByUser(int idUsuario);
        public void Remove(int id);
        public void AsignarAUsuario(int idUsuario, int idTarea);
    }
}