using ClientService.Application.Common.Models.Response;
using ClientService.Application.Files.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ClientService.Application.Files.Commands;

public class UploadFileRequestValidator : AbstractValidator<UploadFileRequest> {
    public UploadFileRequestValidator()
    {
        RuleFor(model => model.File)
            .NotNull();
    }
}
public class UploadFileRequest : IRequest<UploadFileResponse>
{
    public IFormFile File { get; set; }
}