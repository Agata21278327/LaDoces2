using LaDoces2.Models;

namespace LaDoces2.Repositories.Interfaces
{
    public interface IItemRepository
    {
        IEnumerable<Item> Itens { get; }
        IEnumerable<Item> ItensEmDestaque { get; }
        Item GetItemById(int itemId);
    }
}