using ClientService.Application.Posts.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Posts.Queries
{
    public class GetDetailPostRequestValidator : AbstractValidator<GetDetailPostRequest>
    {
        public GetDetailPostRequestValidator()
        {
        }
    }
    public class GetDetailPostRequest : IRequest<PostDetailResponse>
    {
        public long Id { get; set; }
    }
}
