using LaDoces2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LaDoces2.Context
{
    public class AppDbContext : IdentityDbContext<UserAcount>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){
    }
    public DbSet<Categoria> Categorias{get;set;}
    public DbSet<Item> Itens{get;set;}
    public DbSet<CarrinhoItem> CarrinhoItens{get;set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoItem> PedidoItens { get; set; }
    }
}