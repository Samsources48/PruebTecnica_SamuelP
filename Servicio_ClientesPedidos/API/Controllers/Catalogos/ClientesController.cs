using Application.DTOs;
using Application.Features.Clientes.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Plant_HexArquitecture_API.Controllers.Catalogos
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ClientesController(ILogger<ClientesController> logger, IClientesOperation clientesOperation) : ControllerBase
    {
        private readonly ILogger<ClientesController> _logger = logger;
        private readonly IClientesOperation _clientesOperation = clientesOperation;

        [HttpGet()]
        [ProducesResponseType(typeof(List<ClienteDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var data = await _clientesOperation.GetAll();
            return Ok(data);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(List<ClienteDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] SaveClienteDto values)
        {
            var data = await _clientesOperation.Create(values);
            return Ok(data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClienteDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _clientesOperation.GetById(id);
            return Ok(data);
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(ClienteDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] SaveClienteDto values)
        {
            var data = await _clientesOperation.Update(id, values);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ClienteDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _clientesOperation.Delete(id);
            return Ok(data);
        }
    }
}
