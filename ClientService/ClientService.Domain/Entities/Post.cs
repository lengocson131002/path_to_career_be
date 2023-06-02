using ClientService.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientService.Domain.Entities
{
    public class Post : BaseAuditableEntity
    {
        public long Id { get; set; }
        
        public long AccountId { get; set; }

        [ForeignKey("AccountId")] public virtual Account Account { get; set; } = default!;
        
        public long? FreelancerId { get; set; }
        
        public Account? Freelancer { get; set; }

        public long? AcceptedAccountId { get; set; }
        
        [ForeignKey("AcceptedAccountId")]
        public virtual Account? AcceptedAccount { get; set; }

        public PostStatus Status { get; set; } = PostStatus.New;
        
        public long MajorId { get; set; }
        
        [ForeignKey("MajorId")]
        public Major Major { get; set; }
        
        public string JobPosition { get; set; }
        
        public JobLevel JobLevel { get; set; }
        
        public ServiceType ServiceType { get; set; }
        
        public DateTimeOffset? FinishTime { get; set; }
        
        public string Content { get; set; }
        
        public string Title { get; set; }
        
        public int? SupportCount { get; set; }
        
        public string MediaUrl { get; set; }
        
        public CVStyle? CVStyle { get; set; }
        
        public CVType? CVType { get; set; }
        
        public List<PostApplication> PostApplications { get; } = new();

        public string? Description { get; set; }
        
        public long? TransactionId { get; set; }
        
        public Transaction? Transaction { get; set; }
        
    }
}
