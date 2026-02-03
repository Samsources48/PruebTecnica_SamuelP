using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Base;

namespace Domain.Entities
{
    [Table(nameof(Producto), Schema = ("CAT"))]
    public class Producto : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long IdProducto { get; set; }
        [Required, MaxLength(50)]
        public string NombreProducto { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Descripcion { get; set; } = string.Empty;
    }
}
