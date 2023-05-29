using ClientService.Application.Posts.Models;
using ClientService.Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClientService.Application.Posts.Commands
{
    public class UpdatePostRequestValidator : AbstractValidator<UpdatePostRequest>
    {
        public UpdatePostRequestValidator()
        {
            RuleFor(model => model.Title)
           .NotEmpty()
           .When(model => model.Title != null);

            RuleFor(model => model.JobPosition)
           .NotEmpty()
           .When(model => model.JobPosition != null);

            RuleFor(model => model.JobLevel)
           .NotNull()
           .When(model => model.JobLevel != null);

            RuleFor(model => model.ServiceType)
           .NotEmpty()
           .When(model => model.ServiceType != null);

            RuleFor(model => model.Content)
           .NotEmpty()
           .When(model => model.Content != null);

            RuleFor(model => model.MediaUrl)
           .NotEmpty()
           .When(model => model.MediaUrl != null);

            RuleFor(model => model.FinishTime)
             .Must(date => date == null || date > DateTimeOffset.UtcNow)
             .When(model => model.FinishTime != null);
            
            RuleFor(model => model.SupportCount)
             .GreaterThan(0)
             .When(model => model.SupportCount != null);
        }
    }
    public class UpdatePostRequest : IRequest<PostResponse>
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string? MajorCode { get; set; }
        public string? Title { get; set; }
        public string? JobPosition { get; set; }
        public JobLevel? JobLevel { get; set; }
        public ServiceType? ServiceType { get; set; }
        public DateTimeOffset? FinishTime { get; set; }
        public string? Content { get; set; }
        public int? SupportCount { get; set; }
        public string? MediaUrl { get; set; }
        public CVStyle? CVStyle { get; set; }
        public CVType? CVType { get; set; }
        public string? Description { get; set; }
    }
}
