using UniLife.Shared.DataModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Server.Models
{
    public class Tenant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        [Required]
        public Guid OwnerUserId { get; set; }

        public virtual ApplicationUser Owner { get; set; }
    }
}
