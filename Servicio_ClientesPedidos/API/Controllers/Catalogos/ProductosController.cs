using Application.DTOs;
using Application.Features.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace Plant_HexArquitecture_API.Controllers.Catalogos
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductosController(ILogger<ProductosController> logger, IProductsOperation productsOperation) : ControllerBase
    {
        private readonly ILogger<ProductosController> _logger = logger;
        private readonly IProductsOperation _productsOperation = productsOperation;

        [HttpGet()]
        [ProducesResponseType(typeof(List<ProductsDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var data = await _productsOperation.GetAll();
            return Ok(data);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(List<ProductsDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] SaveProductsDto values)
        {
            var data = await _productsOperation.Create(values);
            return Ok(data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductsDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _productsOperation.GetById(id);
            return Ok(data);
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(ProductsDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] SaveProductsDto values)
        {
            var data = await _productsOperation.Update(id, values);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ProductsDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _productsOperation.Delete(id);
            return Ok(data);
        }
    }
}
