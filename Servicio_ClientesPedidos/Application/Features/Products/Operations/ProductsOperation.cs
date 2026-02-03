using Application.DTOs;
using Application.Exceptions;
using Application.Features.Mappings;
using Application.Features.Products.Interfaces;
using Domain.Interfaces.Catalogos;

namespace Application.Features.Products.Operations
{
    public class ProductsOperation(IProductosRepository productosRepository) : IProductsOperation
    {

        public async Task<List<ProductsDto>> GetAll()
        {
            var response = await productosRepository.GetAllAsync();
            return ProductsMapper.Map(response);
        }

        public async Task<ProductsDto> Create(SaveProductsDto dto)
        {
            if (dto == null)
                throw new BadRequestException("El objeto producto no puede ser nulo");

            var producto = ProductsMapper.toEntity(dto);
            var created = await productosRepository.CreateAsync(producto);

            if (created == null)
                throw new BadRequestException("No se pudo guardar el producto");

            return ProductsMapper.toDto(created);
        }

        public async Task<ProductsDto> GetById(int id)
        {
            var producto = await productosRepository.GetByIdAsync(id);
            if (producto == null)
                throw new BadRequestException($"No se encontró el producto con id {id}");

            return ProductsMapper.toDto(producto);
        }

        public async Task<ProductsDto> Update(int id, SaveProductsDto dto)
        {
            if (dto == null)
                throw new BadRequestException("El objeto producto no puede ser nulo");

            var existing = await productosRepository.GetByIdAsync((long)id);
            if (existing == null)
                throw new BadRequestException($"No se encontró el producto con id {id}");

            var productToUpdate = ProductsMapper.toEntity(dto);
            productToUpdate.IdProducto = id;

            var updated = await productosRepository.UpdateAsync(id, productToUpdate);

            if (updated == null)
                throw new BadRequestException("No se pudo actualizar el producto");

            return ProductsMapper.toDto(updated);
        }

        public async Task<ProductsDto> Delete(int id)
        {
            var existing = await productosRepository.GetByIdAsync(id);
            if (existing == null)
                throw new BadRequestException($"No se encontró el producto con id {id}");

            var deleted = await productosRepository.DeleteAsync(id);
            return ProductsMapper.toDto(deleted);
        }
    }
}
