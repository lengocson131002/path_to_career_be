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
        public long AccountId { get; set; }
        public long AcceptedAccountId { get; set; }
        public PostStatus Status { get; set; }
    }
}
