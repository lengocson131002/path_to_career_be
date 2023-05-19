using System.Collections;
using AutoMapper;
using ClientService.Application.Common.Interfaces;
using ClientService.Application.Common.Models.Response;
using ClientService.Application.Majors.Models;
using ClientService.Application.Majors.Queries;
using MediatR;

namespace ClientService.Application.Majors.Handlers;

public class GetAllMajorsHandler : IRequestHandler<GetAllMajorsRequest, ListResponse<MajorResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllMajorsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ListResponse<MajorResponse>> Handle(GetAllMajorsRequest request, CancellationToken cancellationToken)
    {
        var query = await _unitOfWork.MajorRepository.GetAsync(
                major => request.Query == null || (major.Name.ToLower().Contains(request.Query.ToLower()) || major.Code.ToLower().Contains(request.Query.ToLower()))
            );

        var response = _mapper.Map<List<MajorResponse>>(query.ToList());
        
        return new ListResponse<MajorResponse>(response);
    }
}