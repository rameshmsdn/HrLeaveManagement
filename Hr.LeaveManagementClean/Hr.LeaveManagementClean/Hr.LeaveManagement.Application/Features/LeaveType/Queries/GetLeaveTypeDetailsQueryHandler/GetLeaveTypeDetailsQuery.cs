using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetailsQueryHandler;

public record GetLeaveTypeDetailsQuery(int Id): IRequest<LeaveTypeDetailsDto>;