using Hr.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Hr.LeaveManagement.BlazorUI.Services.Base;
using AutoMapper;

namespace Hr.LeaveManagement.BlazorUI.MappingProfiles;

public class Mappingconfig: Profile
{
    public Mappingconfig()
    {
        CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
    }
    
}
