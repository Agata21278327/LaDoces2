using LaDoces2.Models;

namespace LaDoces2.ViewModel
{
    public class PedidoItensViewModel
    {
        public Pedido Pedidos { get; set; }
        public IEnumerable<PedidoItem> PedidoItems { get; set; }
    }
}