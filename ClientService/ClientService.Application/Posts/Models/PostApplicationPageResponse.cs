using ClientService.Application.Common.Models.Response;
using ClientService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Posts.Models
{
    public class PostApplicationPageResponse : PaginationResponse<PostApplication, PostApplicationResponse>
    {
        public PostApplicationPageResponse(IList<PostApplicationResponse> items, int count, int pageNumber, int pageSize) : base(items, count, pageNumber, pageSize)
        {
        }
    }
}
