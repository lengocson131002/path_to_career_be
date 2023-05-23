using ClientService.Application.Posts.Models;
using ClientService.Domain.Enums;
using FluentValidation;
using MediatR;
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
    public class DeletePostRequest : IRequest<DeletePostResponse>
    {
        public long Id { get; set; }
    }
}
