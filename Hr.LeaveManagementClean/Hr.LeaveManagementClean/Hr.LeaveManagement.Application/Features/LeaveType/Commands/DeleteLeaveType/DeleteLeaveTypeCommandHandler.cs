using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public DeleteLeaveTypeCommandHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
    {
        _mapper = mapper;
        _leaveRequestRepository = leaveRequestRepository;
    }
    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // Conver to domain entity object

        var leaveTypeToDelete = await _leaveRequestRepository.GetByIdAsync(request.Id);

        //verify that record exists
        if (leaveTypeToDelete == null)
            throw new NotFoundException(nameof(LeaveType),request.Id);

        // add to database
        await _leaveRequestRepository.DeleteAsync(leaveTypeToDelete);

        return Unit.Value;
    }
}
