using Application.DTOs;
using Domain.Entities;

namespace Application.Features.Mappings
{
    public static class PedidoMapper
    {
        public static PedidoDto toDto(Pedido entity)
        {
            if (entity == null) return new PedidoDto();
            return new PedidoDto
            {
                IdPedido = entity.IdPedido,
                Descripcion = entity.Descripcion,
                FechaPedido = entity.FechaPedido,
                Total = entity.Total,
                Estado = entity.Estado,
                IdCliente = entity.IdCliente,
                NombreCliente = entity.Cliente != null ? $"{entity.Cliente.Nombre} {entity.Cliente.Apellido}" : null,
                Activo = entity.Activo,
                FechaRegistro = entity.FechaRegistro,
                Detalles = entity.Detalles != null 
                    ? entity.Detalles.Select(d => toDetalleDto(d)).ToList() 
                    : new List<DetallePedidoDto>()
            };
        }

        public static DetallePedidoDto toDetalleDto(DetallePedido entity)
        {
            if (entity == null) return new DetallePedidoDto();
            return new DetallePedidoDto
            {
                IdDetallePedido = entity.IdDetallePedido,
                IdProducto = entity.IdProducto,
                Cantidad = entity.Cantidad,
                PrecioUnitario = entity.PrecioUnitario,
                Subtotal = entity.Cantidad * entity.PrecioUnitario
            };
        }

        public static Pedido toEntity(SavePedidoDto dto)
        {
            if (dto == null) return new Pedido();
            var pedido = new Pedido
            {
                Descripcion = dto.Descripcion,
                IdCliente = dto.IdCliente,
                Estado = !string.IsNullOrEmpty(dto.Estado) ? dto.Estado : "Pendiente",
                FechaPedido = dto.FechaPedido ?? DateTime.Now,
                Activo = true,
                FechaRegistro = DateTime.Now,
                Detalles = dto.Detalles != null 
                    ? dto.Detalles.Select(d => toDetalleEntity(d)).ToList() 
                    : new List<DetallePedido>()
            };
            
            if (pedido.Detalles.Any())
            {
                pedido.Total = pedido.Detalles.Sum(d => d.Subtotal);
            }
            return pedido;
        }

        public static DetallePedido toDetalleEntity(SaveDetallePedidoDto dto)
        {
            if (dto == null) return new DetallePedido();
            
            var detalle = new DetallePedido
            {
                IdProducto = dto.IdProducto,
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario,
                Activo = true,
                FechaRegistro = DateTime.Now
            };
            
            detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;
            return detalle;
        }

        public static List<PedidoDto> Map(List<Pedido> pedidos)
        {
            return [.. pedidos.Select(p => toDto(p))];
        }
    }
}
