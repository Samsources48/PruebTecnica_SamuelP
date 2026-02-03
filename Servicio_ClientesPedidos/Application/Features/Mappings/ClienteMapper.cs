using Application.DTOs;
using Domain.Entities;

namespace Application.Features.Mappings
{
    public static class ClienteMapper
    {
        public static ClienteDto toDto(Cliente entity)
        {
            if (entity == null) return new ClienteDto();
            return new ClienteDto
            {
                IdCliente = entity.IdCliente,
                Nombre = entity.Nombre,
                Apellido = entity.Apellido,
                Email = entity.Email,
                Telefono = entity.Telefono,
                Direccion = entity.Direccion,
                Activo = entity.Activo,
                FechaRegistro = entity.FechaRegistro
            };
        }

        public static Cliente toEntity(SaveClienteDto dto)
        {
            if (dto == null) return new Cliente();
            return new Cliente
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Email = dto.Email,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                Activo = true,
                FechaRegistro = DateTime.Now
            };
        }

        public static List<ClienteDto> Map(List<Cliente> clientes)
        {
            return [.. clientes.Select(c => toDto(c))];
        }
    }
}
