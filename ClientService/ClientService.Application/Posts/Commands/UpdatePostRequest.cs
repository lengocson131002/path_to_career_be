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
        }
    }
    public class UpdatePostRequest : IRequest<PostResponse>
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public PostStatus Status { get; set; }
        public long MajorId { get; set; }
        public string JobPosition { get; set; }
        public string JobLevel { get; set; }
        public string ServiceType { get; set; }
        public DateTime FinishTime { get; set; }
        public string Content { get; set; }
        public int SupportCount { get; set; }
        public string MediaUrl { get; set; }
        public string? CVStyle { get; set; }
        public string? CVTyle { get; set; }
    }
}
