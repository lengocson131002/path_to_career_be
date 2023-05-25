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
        [Column(TypeName = "varchar(20)")]
        public PostStatus Status { get; set; }
        public long MajorId { get; set; }
        [ForeignKey("MajorId")]
        public Major Major { get; set; }
        public string JobPosition { get; set; }
        public string JobLevel { get; set; }
        [Column(TypeName = "varchar(20)")]
        public ServiceType ServiceType { get; set; }
        public DateTime FinishTime { get; set; }
        public string Content { get; set; }
        public int SupportCount { get; set; }
        public string MediaUrl { get; set; }
        [Column(TypeName = "varchar(20)")]
        public CVStyle CVStyle { get; set; }
        [Column(TypeName = "varchar(20)")]
        public CVType CVType { get; set; }
        public List<PostApplication> PostApplications { get; } = new();
    }
}
