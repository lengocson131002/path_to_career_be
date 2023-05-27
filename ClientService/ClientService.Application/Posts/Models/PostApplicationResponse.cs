using ClientService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Posts.Models
{
    public class PostApplicationResponse
    {
        public long Id { get; set; }
        public long ApplierId { get; set; }
        public long PostId { get; set; }
        public long? SupportCount { get; set; }
        public decimal? FeePerCount { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
        public string? ExperienceDescription { get; set; }
        public string? MethodDescription { get; set; }
    }
}
