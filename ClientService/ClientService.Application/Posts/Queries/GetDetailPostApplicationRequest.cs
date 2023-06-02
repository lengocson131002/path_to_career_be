using ClientService.Application.Posts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClientService.Application.Posts.Queries
{
    public class GetDetailPostApplicationRequest : IRequest<PostApplicationResponse>
    {
        [JsonIgnore]
        public long PostId { get; set; }
        public long ApplicationId { get; set;}
    }
}
