using Domain.Entities;
using Domain.Interfaces.Catalogos;
using Domain.Repository.Base;

namespace Domain.Repository.Catalogo
{
    public class PedidoRepository(SqlDbContext sqlDbContext) : Repository<Pedido>(sqlDbContext), IPedidoRepository
    {
    }
}
