﻿using FluentValidation;
using FluentValidation.Validators;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandValidator: AbstractValidator<CreateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        this._leaveTypeRepository = leaveTypeRepository;
        RuleFor(p => p.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName} does not exist.");
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken args)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
        return leaveType != null;
    }
}
