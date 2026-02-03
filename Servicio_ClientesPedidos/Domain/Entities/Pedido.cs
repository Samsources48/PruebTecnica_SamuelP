using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table(nameof(Pedido), Schema = ("CAT"))]
public class Pedido : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long IdPedido { get; set; }
    public string? Descripcion { get; set; } = string.Empty;
    public DateTime FechaPedido { get; set; }

    [Column(TypeName = "decimal(14,2)")]
    public decimal Total { get; set; }

    [MaxLength(50)]
    public string Estado { get; set; } = "Pendiente";
    public long IdCliente { get; set; }

    [ForeignKey("IdCliente")]
    public virtual Cliente? Cliente { get; set; }

    public virtual IList<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
}
