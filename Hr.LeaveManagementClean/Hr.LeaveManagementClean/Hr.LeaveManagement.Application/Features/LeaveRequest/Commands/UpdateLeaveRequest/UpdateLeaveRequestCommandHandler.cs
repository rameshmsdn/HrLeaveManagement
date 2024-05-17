﻿using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Email;
using Hr.LeaveManagement.Application.Contracts.Logging;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.Exceptions;
using Hr.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler: IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _appLogger;

    public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository, IMapper mapper,
        IEmailSender emailSender, IAppLogger<UpdateLeaveRequestCommandHandler> appLogger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
        _emailSender = emailSender;
        _appLogger = appLogger;
    }

    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
        if (leaveRequest is null)
            throw new NotFoundException(nameof(LeaveRequest),request.Id);

        var validator = new UpdateLeaveRequestCommandValidator(_leaveTypeRepository, _leaveRequestRepository);

        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid Leave Request", validationResult);

        _mapper.Map(request, leaveRequest);
         await _leaveRequestRepository.UpdateAsync(leaveRequest);

        // Send Confirmation email
        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D}" + $"has been updated successfully.",
                Subject = "Leave Request Submitted"
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {

            _appLogger.LogWarning(ex.Message);
        }
        

        return Unit.Value;

    }
}
