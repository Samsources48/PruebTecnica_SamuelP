using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table(nameof(Cliente), Schema = ("CAT"))]

public class Cliente : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long IdCliente { get; set; }

    [Required]
    [MaxLength(200)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Apellido { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(10)]
    public string? Telefono { get; set; }

    [MaxLength(250)]
    public string? Direccion { get; set; }
    public virtual IList<Pedido>? Pedidos { get; set; } = new List<Pedido>();
}
