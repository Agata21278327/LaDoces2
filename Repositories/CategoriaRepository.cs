using LaDoces2.Context;
using LaDoces2.Models;
using LaDoces2.Repositories.Interfaces;

namespace LaDoces2.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}