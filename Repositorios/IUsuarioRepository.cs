namespace tl2_tp10_2023_ezemrtz.Repositorios{
    public interface IUsuarioRepository{
        public void Create(Usuario usuario);
        public void Update(int id, Usuario usuario);
        public List<Usuario> GetAll();
        public Usuario Get(int id);
        public Usuario GetByNamePassword(string nombre, string contrasenia);
        public Usuario GetByName(string nombre);
        public bool ExistsByName(string nombre);
        public void Remove(int id);
    }
}