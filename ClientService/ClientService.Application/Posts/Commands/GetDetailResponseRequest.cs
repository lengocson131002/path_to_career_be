using ClientService.Application.Posts.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Posts.Commands
{
    public class GetDetailPostRequestValidator : AbstractValidator<GetDetailPostRequest>
    {
        public GetDetailPostRequestValidator()
        {
        }
    }
    public class GetDetailPostRequest : IRequest<PostResponse>
    {
        public long Id { get; set; }
    }
}
