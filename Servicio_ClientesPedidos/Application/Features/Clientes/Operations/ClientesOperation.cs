using Application.DTOs;
using Application.Exceptions;
using Application.Features.Mappings;
using Application.Features.Clientes.Interfaces;
using Domain.Interfaces.Catalogos;

namespace Application.Features.Clientes.Operations
{
    public class ClientesOperation(IClienteRepository clienteRepository) : IClientesOperation
    {
        public async Task<List<ClienteDto>> GetAll()
        {
            var response = await clienteRepository.GetAllAsync();
            return ClienteMapper.Map(response);
        }

        public async Task<ClienteDto> Create(SaveClienteDto dto)
        {
            if (dto == null)
                throw new BadRequestException("El objeto cliente no puede ser nulo");

            var cliente = ClienteMapper.toEntity(dto);
            var created = await clienteRepository.CreateAsync(cliente);

            if (created == null)
                throw new BadRequestException("No se pudo guardar el cliente");

            return ClienteMapper.toDto(created);
        }

        public async Task<ClienteDto> GetById(int id)
        {
            var cliente = await clienteRepository.GetByIdAsync(id);
            if (cliente == null)
                throw new BadRequestException($"No se encontró el cliente con id {id}");
            
            return ClienteMapper.toDto(cliente);
        }

        public async Task<ClienteDto> Update(int id, SaveClienteDto dto)
        {
            if (dto == null)
                throw new BadRequestException("El objeto cliente no puede ser nulo");

            var existing = await clienteRepository.ExistsAsync((long)id);
            if (!existing)
                throw new BadRequestException($"No se encontró el cliente con id {id}");

            // Basic mapping for update - creates a new entity with the data to update
            // The repository's UpdateAsync(id, entity) likely handles the merge or update logic
            var clienteToUpdate = ClienteMapper.toEntity(dto);
            clienteToUpdate.IdCliente = id; // Ensure ID is set
            
            var updated = await clienteRepository.UpdateAsync(id, clienteToUpdate);

            if (updated == null)
                throw new BadRequestException("No se pudo actualizar el cliente");

            return ClienteMapper.toDto(updated);
        }

        public async Task<ClienteDto> Delete(int id)
        {
            var existing = await clienteRepository.GetByIdAsync(id);
            if (existing == null)
                throw new BadRequestException($"No se encontró el cliente con id {id}");

            var deleted = await clienteRepository.DeleteAsync(id);
            return ClienteMapper.toDto(deleted);
        }
    }
}
