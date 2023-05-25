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
        }
    }
    public class CreatePostRequest : IRequest<PostResponse>
    {
       
        public PostStatus Status { get; set; }
        public long MajorId { get; set; }
        public string JobPosition { get; set; }
        public string JobLevel { get; set; }
        public ServiceType ServiceType { get; set; }
        public DateTime FinishTime { get; set; }
        public string Content { get; set; }
        public int SupportCount { get; set; }
        public string MediaUrl { get; set; }
        public CVStyle CVStyle { get; set; }
        public CVType CVType { get; set; }
    }
}
