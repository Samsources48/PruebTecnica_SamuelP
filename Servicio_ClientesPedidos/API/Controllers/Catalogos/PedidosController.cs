using Application.DTOs;
using Application.Features.Pedidos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Plant_HexArquitecture_API.Controllers.Catalogos
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PedidosController(ILogger<PedidosController> logger, IPedidosOperation pedidosOperation) : ControllerBase
    {
        private readonly ILogger<PedidosController> _logger = logger;
        private readonly IPedidosOperation _pedidosOperation = pedidosOperation;

        [HttpGet()]
        [ProducesResponseType(typeof(List<PedidoDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var data = await _pedidosOperation.GetAll();
            return Ok(data);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(List<PedidoDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] SavePedidoDto values)
        {
            var data = await _pedidosOperation.Create(values);
            return Ok(data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PedidoDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _pedidosOperation.GetById(id);
            return Ok(data);
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PedidoDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] SavePedidoDto values)
        {
            var data = await _pedidosOperation.Update(id, values);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(PedidoDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _pedidosOperation.Delete(id);
            return Ok(data);
        }
    }
}
