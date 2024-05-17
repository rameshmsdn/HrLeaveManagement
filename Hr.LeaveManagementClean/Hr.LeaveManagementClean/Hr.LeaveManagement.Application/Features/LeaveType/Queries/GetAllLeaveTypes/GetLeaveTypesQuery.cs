using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    //public class GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>  // IRequest is MediatR interface
    //{
    //}

    //or 
    public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;
}
