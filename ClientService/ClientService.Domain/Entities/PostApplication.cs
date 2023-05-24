

using System.ComponentModel.DataAnnotations.Schema;

namespace ClientService.Domain.Entities
{
    public class PostApplication : BaseAuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long PostId { get; set; }
        public long ApplierId { get; set; }
        [ForeignKey("ApplierId")]
        public virtual Account Applier { get; set; } = null!;
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; } = null!;
        public long SupportCount { get; set; }
        public decimal FeePerCount { get; set; }
        public string? ExperienceDescription { get; set; }
        public string? MethodDescription { get; set; }

    }
}
