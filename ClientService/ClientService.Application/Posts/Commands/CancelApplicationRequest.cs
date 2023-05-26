using ClientService.Application.Common.Models.Response;
using ClientService.Application.Posts.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Posts.Commands
{
    public class CancelApplicationRequest : IRequest<PostApplicationResponse>
    {
        public long PostId { get; set; }
        public long ApplicationId { get; set; }
    }
}
