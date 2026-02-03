using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.seguridad
{
    [Table(nameof(User), Schema ="SEG")]
    public class User : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [StringLength(100)]
        public string? GuidUser { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;

        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    }
}
