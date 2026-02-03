using Application.DTOs;
using Application.Exceptions;
using Application.Features.Mappings;
using Application.Features.Pedidos.Interfaces;
using Domain.Interfaces.Catalogos;

namespace Application.Features.Pedidos.Operations
{
    public class PedidosOperation(IPedidoRepository pedidoRepository) : IPedidosOperation
    {
        public async Task<List<PedidoDto>> GetAll()
        {
            var response = await pedidoRepository.GetAllAsync(x => x.Activo, p => p.Cliente);
            return PedidoMapper.Map(response);
        }

        public async Task<PedidoDto> Create(SavePedidoDto dto)
        {
            if (dto == null)
                throw new BadRequestException("El objeto pedido no puede ser nulo");

            var pedido = PedidoMapper.toEntity(dto);
            var created = await pedidoRepository.CreateAsync(pedido);

            if (created == null)
                throw new BadRequestException("No se pudo guardar el pedido");

            return PedidoMapper.toDto(created);
        }

        public async Task<PedidoDto> GetById(int id)
        {
            var pedido = await pedidoRepository.GetByIdAsync(id);
            if (pedido == null)
                throw new BadRequestException($"No se encontró el pedido con id {id}");

            return PedidoMapper.toDto(pedido);
        }

        public async Task<PedidoDto> Update(int id, SavePedidoDto dto)
        {
            if (dto == null)
                throw new BadRequestException("El objeto pedido no puede ser nulo");

            var existing = await pedidoRepository.GetByIdAsync(id);
            if (existing == null)
                throw new BadRequestException($"No se encontró el pedido con id {id}");

            var pedidoToUpdate = PedidoMapper.toEntity(dto);
            pedidoToUpdate.IdPedido = id;

            var updated = await pedidoRepository.UpdateAsync(id, pedidoToUpdate);

            if (updated == null)
                throw new BadRequestException("No se pudo actualizar el pedido");

            return PedidoMapper.toDto(updated);
        }

        public async Task<PedidoDto> Delete(int id)
        {
            var existing = await pedidoRepository.GetByIdAsync(id);
            if (existing == null)
                throw new BadRequestException($"No se encontró el pedido con id {id}");

            var deleted = await pedidoRepository.DeleteAsync(id);
            return PedidoMapper.toDto(deleted);
        }
    }
}
