using ClientService.Application.Posts.Models;
using ClientService.Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Posts.Commands
{
    public class UpdatePostRequestValidator : AbstractValidator<UpdatePostRequest>
    {
        public UpdatePostRequestValidator()
        {
            RuleFor(model => model.AccountId)
           .NotNull();

            RuleFor(model => model.MajorId)
           .NotNull();

            RuleFor(model => model.Title)
           .NotEmpty();

            RuleFor(model => model.JobPosition)
           .NotEmpty();

            RuleFor(model => model.JobLevel)
           .NotEmpty();

            RuleFor(model => model.ServiceType)
           .NotEmpty();

            RuleFor(model => model.Content)
           .NotEmpty();

            RuleFor(model => model.MediaUrl)
           .NotEmpty();
        }
    }
    public class UpdatePostRequest : IRequest<PostResponse>
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public long MajorId { get; set; }
        public string Title { get; set; }
        public string JobPosition { get; set; }
        public string JobLevel { get; set; }
        public ServiceType ServiceType { get; set; }
        public DateTimeOffset? FinishTime { get; set; }
        public string Content { get; set; }
        public int? SupportCount { get; set; }
        public string MediaUrl { get; set; }
        public CVStyle? CVStyle { get; set; }
        public CVType? CVType { get; set; }
    }
}
