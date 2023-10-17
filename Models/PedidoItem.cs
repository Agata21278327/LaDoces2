using System.ComponentModel.DataAnnotations.Schema;

namespace LaDoces2.Models
{
    public class PedidoItem
    {
        public int PedidoItemId { get; set; }
        public int PedidoId { get; set; }
        public int ItemId { get; set; }
        public int Quantidade { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }
        public virtual Item Item { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}