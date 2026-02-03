using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table(nameof(DetallePedido), Schema = ("CAT"))]
    public class DetallePedido: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdDetallePedido { get; set; }
        public int Cantidad { get; set; }
        [Column(TypeName = "decimal(14,2)")]
        public decimal PrecioUnitario { get; set; }
        public long IdPedido { get; set; }
        [ForeignKey("IdPedido")]
        public virtual Pedido? Pedido { get; set; }
        public long IdProducto { get; set; }
        [ForeignKey("IdProducto")]
        public virtual Producto? Producto { get; set; }
    }
}
