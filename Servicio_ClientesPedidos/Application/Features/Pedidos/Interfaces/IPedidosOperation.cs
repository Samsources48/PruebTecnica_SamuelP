using Application.DTOs;

namespace Application.Features.Pedidos.Interfaces
{
    public interface IPedidosOperation
    {
        Task<PedidoDto> Create(SavePedidoDto dto);
        Task<List<PedidoDto>> GetAll();
        Task<PedidoDto> GetById(int id);
        Task<PedidoDto> Update(int id, SavePedidoDto dto);
        Task<PedidoDto> Delete(int id);
    }
}
