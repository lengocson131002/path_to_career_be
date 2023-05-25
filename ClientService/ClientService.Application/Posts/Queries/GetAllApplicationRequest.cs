using ClientService.Application.Posts.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.Application.Posts.Queries
{
    public class GetAllApplicationRequest : IRequest<PostApplicationPageResponse>
    {
        [FromRoute]
        public long PostId { get; set; }
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}
