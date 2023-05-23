﻿using ClientService.Application.Posts.Models;
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
    public class ApplyPostRequestValidator : AbstractValidator<ApplyPostRequest>
    {
        public ApplyPostRequestValidator()
        {
        }
    }
    public class ApplyPostRequest : IRequest<ApplyPostResponse>
    {
        public long ApplierId { get; set; }
        public long PostId { get; set; }
        public long SupportCount { get; set; }
        public decimal FeePerCount { get; set; }
        public string? ExperienceDescription { get; set; }
        public string? MethodDescription { get; set;}
    }
}
