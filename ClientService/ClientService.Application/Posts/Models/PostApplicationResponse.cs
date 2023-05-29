using ClientService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientService.Application.Accounts.Models;

namespace ClientService.Application.Posts.Models
{
    public class PostApplicationResponse
    {
        public long Id { get; set; }

        public AccountResponse Applier { get; set; } = default!;

        public PostResponse Post { get; set; } = default!;
        
        public long? SupportCount { get; set; }
        
        public decimal? FeePerCount { get; set; }
        
        public ApplicationStatus ApplicationStatus { get; set; }
        
        public string? ExperienceDescription { get; set; }
        
        public string? MethodDescription { get; set; }

        public DateTimeOffset? CreatedAt;

        public DateTimeOffset? UpdatedAt;
    }
}
