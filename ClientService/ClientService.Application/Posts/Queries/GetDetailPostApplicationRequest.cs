using ClientService.Application.Posts.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Posts.Queries
{
    public class GetDetailPostApplicationRequest : IRequest<PostApplicationResponse>
    {
        public long PostId { get; set; }
        public long ApplicationId { get; set;}
    }
}
