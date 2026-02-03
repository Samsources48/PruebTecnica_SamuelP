using Domain.Entities;
using Domain.Interfaces.Catalogos;
using Domain.Repository.Base;

namespace Domain.Repository.Catalogo
{
    public class ClienteRepository(SqlDbContext sqlDbContext) : Repository<Cliente>(sqlDbContext), IClienteRepository
    {
    }
}
