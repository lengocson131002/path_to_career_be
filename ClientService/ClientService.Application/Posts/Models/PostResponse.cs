using ClientService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Posts.Models
{
    public class PostResponse 
    {
        public long Id { get; set; }

        public string Title { get; set; } = default!;
        
        public long AccountId { get; set; }
        
        public long? AcceptedAccountId { get; set; }
        
        public PostStatus Status { get; set; }
        
        public long MajorId { get; set; }
        
        public string JobPosition { get; set; } = default!;
        
        public JobLevel JobLevel { get; set; }
        
        public ServiceType ServiceType { get; set; }
        
        public DateTimeOffset? FinishTime { get; set; }
        
        public string Content { get; set; }
        
        public int? SupportCount { get; set; }
        
        public string MediaUrl { get; set; }
        
        public CVStyle? CVStyle { get; set; }
        
        public CVType? CVType { get; set; }
        
        public DateTimeOffset? CreatedAt;
        
        public DateTimeOffset? UpdatedAt;
    }
}
