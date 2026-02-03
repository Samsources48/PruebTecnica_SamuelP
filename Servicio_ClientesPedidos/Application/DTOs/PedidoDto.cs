using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class PedidoDto : AuditProperties
    {
        public long IdPedido { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaPedido { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = string.Empty;
        public long IdCliente { get; set; }
        public List<DetallePedidoDto> Detalles { get; set; } = new List<DetallePedidoDto>();
    }

    public class SavePedidoDto
    {
        public string? Descripcion { get; set; }
        public int IdCliente { get; set; }
        public List<SaveDetallePedidoDto> Detalles { get; set; } = new List<SaveDetallePedidoDto>();
    }

    public class DetallePedidoDto
    {
        public long IdDetallePedido { get; set; }
        public long IdProducto { get; set; }
        public long Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class SaveDetallePedidoDto
    {
        public long IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
