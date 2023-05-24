using ClientService.Application.Common.Models.Response;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Posts.Commands
{
    public class AcceptApplicationRequestValidator : AbstractValidator<AcceptApplicationRequest>
    {
        public AcceptApplicationRequestValidator()
        {
        }
    }
    public class AcceptApplicationRequest : IRequest<StatusResponse>
    {
        public long PostId { get; set; }
        public long ApplicationId { get; set; }
    }
}
