using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class SqlDbContext(DbContextOptions<SqlDbContext> options): DbContext(options)
    {
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<DetallePedido> DetallesPedido { get; set; }

    }
}
