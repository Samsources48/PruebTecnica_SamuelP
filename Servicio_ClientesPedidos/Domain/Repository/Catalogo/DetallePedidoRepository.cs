using Domain.Entities;
using Domain.Interfaces.Catalogos;
using Domain.Repository.Base;

namespace Domain.Repository.Catalogo
{
    public class DetallePedidoRepository(SqlDbContext sqlDbContext) : Repository<DetallePedido>(sqlDbContext), IDetallePedidoRepository
    {
    }
}
