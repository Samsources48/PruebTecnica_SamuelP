using Application.DTOs;

namespace Application.Features.Products.Interfaces
{
    public interface IProductsOperation
    {
        Task<ProductsDto> Create(SaveProductsDto dto);
        Task<List<ProductsDto>> GetAll();
        Task<ProductsDto> GetById(int id);
        Task<ProductsDto> Update(int id, SaveProductsDto dto);
        Task<ProductsDto> Delete(int id);
    }
}
