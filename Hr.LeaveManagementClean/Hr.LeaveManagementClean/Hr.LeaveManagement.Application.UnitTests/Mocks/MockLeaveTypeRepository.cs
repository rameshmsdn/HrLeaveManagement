﻿using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Application.UnitTests.Mocks;

public class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()
    {
        var leaveTypes = new List<LeaveType>
        {
            new LeaveType
            {
                Id = 1,
                DefaultDays = 10,
                Name="Test Vacation"
            },
            new LeaveType
            {
                Id = 2,
                DefaultDays = 15,
                Name="Test Sick"
            },
            new LeaveType
            {
                Id = 3,
                DefaultDays = 15,
                Name="Test Maternity"
            }
        };

        var mockRepo= new Mock<ILeaveTypeRepository>();

        mockRepo.Setup(r =>r.GetAsync()).ReturnsAsync(leaveTypes); // GetAsync is ILeaveTypeRepository Method and return local/mocked leavetypes instead of actual GetAsync data.

        mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>())).Returns((LeaveType leavetype) =>
        {
            leaveTypes.Add(leavetype);
            return Task.CompletedTask;
        });
        return mockRepo;
    }
}
