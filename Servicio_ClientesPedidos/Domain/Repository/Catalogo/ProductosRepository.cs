using Domain.Entities;
using Domain.Interfaces.Catalogos;
using Domain.Repository.Base;

namespace Domain.Repository.Catalogo
{
    public class ProductosRepository(SqlDbContext sqlDbContext) : Repository<Producto>(sqlDbContext), IProductosRepository
    {
    }
}
