using Application.DTOs;

namespace Application.Features.Clientes.Interfaces
{
    public interface IClientesOperation
    {
        Task<ClienteDto> Create(SaveClienteDto dto);
        Task<List<ClienteDto>> GetAll();
        Task<ClienteDto> GetById(int id);
        Task<ClienteDto> Update(int id, SaveClienteDto dto);
        Task<ClienteDto> Delete(int id);
    }
}
