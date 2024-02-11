namespace tl2_tp10_2023_ezemrtz.Repositorios{
    public interface ITableroRepository{
        public void Create(Tablero tablero);
        public void Update(int id, Tablero tablero);
        public List<Tablero> GetAll();
        public Tablero Get(int id);
        public List<Tablero> GetByUser(int idUsuario);
        public List<Tablero> GetByAssignedTask(int idUsuario);
        public void Remove(int id);
    }
}