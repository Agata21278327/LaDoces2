using LaDoces2.Models;

namespace LaDoces2.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        public IEnumerable<Categoria> Categorias {get;}
    }
}