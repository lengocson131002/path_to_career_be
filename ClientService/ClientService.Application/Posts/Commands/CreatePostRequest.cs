using ClientService.Application.Posts.Models;
using ClientService.Domain.Enums;
using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Posts.Commands
{
    public class CreatePostRequestValidator : AbstractValidator<CreatePostRequest>
    {
        public CreatePostRequestValidator()
        {
           RuleFor(model => model.Title)
           .NotEmpty();

            RuleFor(model => model.JobPosition)
           .NotEmpty();

            RuleFor(model => model.JobLevel)
           .NotNull();

            RuleFor(model => model.ServiceType)
           .NotNull();

            RuleFor(model => model.Content)
           .NotEmpty();

            RuleFor(model => model.MediaUrl)
           .NotEmpty();

            RuleFor(model => model.FinishTime)
             .Must(date => date == null || date > DateTimeOffset.UtcNow);

            RuleFor(model => model.MajorCode)
             .NotEmpty();
        }
    }
    public class CreatePostRequest : IRequest<PostResponse>
    {
        public string Title { get; set; }
        
        public string JobPosition { get; set; }
        
        public JobLevel JobLevel { get; set; }
        public ServiceType ServiceType { get; set; }
        
        public DateTimeOffset? FinishTime { get; set; }
        public string Content { get; set; }
        
        public int? SupportCount { get; set; }
        public string MediaUrl { get; set; }

        public CVStyle? CVStyle { get; set; }
        
        public CVType? CVType { get; set; }

        public string MajorCode { get; set; } = default!;

        public string? Description { get; set; }
    }
}
