using ClientService.Application.Posts.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Posts.Commands
{
    public class DeletePostRequestValidator : AbstractValidator<DeletePostRequest>
    {
        public DeletePostRequestValidator()
        {
        }
    }
    public class DeletePostRequest : IRequest<StatusResponse>
    {
        public long Id { get; set; }
    }
}
