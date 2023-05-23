

using System.ComponentModel.DataAnnotations.Schema;

namespace ClientService.Domain.Entities
{
    public class PostApplication : BaseAuditableEntity
    {
        public long PostId { get; set; }
        public long ApplierId { get; set; }
        [ForeignKey("ApplierId")]
        public virtual Account Applier { get; set; } = null!;
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; } = null!;


    }
}
