using Application.DTOs;
using Domain.Entities;

namespace Application.Features.Mappings
{
    public static class ProductsMapper
    {
        public static ProductsDto toDto(Producto entity)
        {
            if (entity == null) return new ProductsDto();
            return new ProductsDto
            {
                IdProducto = entity.IdProducto,
                NombreProducto = entity.NombreProducto,
                Descripcion = entity.Descripcion,
                Activo = entity.Activo,
                FechaRegistro = entity.FechaRegistro
            };
        }

        public static Producto toEntity(SaveProductsDto dto)
        {
            if (dto == null) return new Producto();
            return new Producto
            {
                //IdProducto = dto.IdProducto,
                NombreProducto = dto.NombreProducto,
                Descripcion = dto.Descripcion,
                Activo = true,
                FechaRegistro = DateTime.Now
            };
        }

        public static List<ProductsDto> Map(List<Producto> productos)
        {
            return [..productos.Select(p => toDto(p))];
        }
    }
}
