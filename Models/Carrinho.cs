using LaDoces2.Context;
using Microsoft.EntityFrameworkCore;

namespace LaDoces2.Models
{
    public class Carrinho
    {
        private readonly AppDbContext _context;
        public Carrinho(AppDbContext context)
        {
            _context = context;
        }

        public string CarrinhoId { get; set; }
        public List<CarrinhoItem> CarrinhoItens { get; set; }
        public static Carrinho GetCarrinhoCompra(IServiceProvider services){

            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<AppDbContext>();
            string carrinhoId = session.GetString("CarrinhoId") ??

            Guid.NewGuid().ToString();

            session.SetString("CarrinhoId", carrinhoId);
            return new Carrinho(context)
            {
                CarrinhoId = carrinhoId
            };

        }
        public void AdicionarItemCarrinho(Item item)
        {
            var carrinhoItem = _context.CarrinhoItens.SingleOrDefault(s => s.Item.ItemId == item.ItemId && s.CarrinhoId == CarrinhoId);

            if (carrinhoItem == null)
            {
                carrinhoItem = new CarrinhoItem
                {
                    CarrinhoId = CarrinhoId,
                    Item = item,
                    Quantidade = 1
                };
                _context.CarrinhoItens.Add(carrinhoItem);
            }
            else
            {
                carrinhoItem.Quantidade++;
            }
            _context.SaveChanges();
        }
        public int RemoverItemDoCarrinhoCompra(Item item)
        {
            var carrinhoQuantidade = 0;
            var carrinhoItem = _context.CarrinhoItens.SingleOrDefault(s => s.Item.ItemId == item.ItemId && s.CarrinhoId == CarrinhoId);

            if (carrinhoItem != null)
            {
                if (carrinhoItem.Quantidade > 1)
                {
                    carrinhoItem.Quantidade--;
                    carrinhoQuantidade = carrinhoItem.Quantidade;
                }
                else
                {
                    _context.CarrinhoItens.Remove(carrinhoItem);
                }

            }
            _context.SaveChanges();
            return carrinhoQuantidade;
        }
        public List<CarrinhoItem> GetCarrinhoCompraItems()
        {
            return CarrinhoItens ?? _context.CarrinhoItens.Where(c => c.CarrinhoId == CarrinhoId).Include(s => s.Item).ToList();
        } 
        public void LimparCarrinho()
        {
            var carrinhoCompra = _context.CarrinhoItens.Where(c => c.CarrinhoId == CarrinhoId);

            _context.CarrinhoItens.RemoveRange(carrinhoCompra);
            _context.SaveChanges();
        }
        public double GetCarrinhoCompraTotal()
        {
            List<double> total = _context.CarrinhoItens.Where(_c => _c.CarrinhoId == CarrinhoId).Select(c => c.Quantidade * c.Item.Preco).ToList();
            double totalr = 0;
            foreach (double t in total)
            {
                totalr = totalr + t;
            }
            return totalr;
        }
    }
}