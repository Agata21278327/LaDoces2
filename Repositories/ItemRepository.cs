using LaDoces2.Context;
using LaDoces2.Models;
using LaDoces2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LaDoces2.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;
        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Item> Itens => _context.Itens.Include(c => c.Categoria);
        public IEnumerable<Item> ItensEmDestaque => _context.Itens.Where(i => i.Destaque).Include(c => c.Categoria);
        public Item GetItemById(int itemId)
        {
            return _context.Itens.FirstOrDefault(m => m.ItemId == itemId);
        }
    }
}