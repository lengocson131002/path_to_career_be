using ClientService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Domain.Entities
{
    public class Post : BaseAuditableEntity
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public long? AcceptedAccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
        [ForeignKey("AcceptedAccountId")]
        public virtual Account? AcceptedAccount { get; set; }
        public PostStatus Status { get; set; }
        public long MajorId { get; set; }
        public string JobPosition { get; set; }
        public string JobLevel { get; set; }
        public string ServiceType { get; set; }
        public DateTime FinishTime { get; set; }
        public string Content { get; set; }
        public int SupportCount { get; set; }
        public string MediaUrl { get; set; }
        public string? CVStlye { get; set; }
        public string? CVTyle { get; set; }
        public List<PostApplication> PostApplications { get; } = new();
    }
}
