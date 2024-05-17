using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Hr.LeaveManagement.BlazorUI.Services.Base;
using AutoMapper;
using Blazored.LocalStorage;

namespace Hr.LeaveManagement.BlazorUI.Services;

public class LeaveTypeService : BaseHttpService, ILeaveTypeService
{
    private readonly IMapper _mapper;

    public LeaveTypeService(IClient client, IMapper mapper, ILocalStorageService localStorage) : base(client, localStorage)
    {
        _mapper = mapper;
    }

    public async Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveTypevm)
    {
        //throw new NotImplementedException();
        try
        {
            await AddBearerToken();
            var createLeaveTypeCommand = _mapper.Map<CreateLeaveTypeCommand>(leaveTypevm);
            await _client.LeaveTypesPOSTAsync(createLeaveTypeCommand); ;
            return new Response<Guid>()
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> DeleteLeaveType(int id)
    {
        try
        {
            await AddBearerToken();
            await _client.LeaveTypesDELETEAsync(id);
            return new Response<Guid>()
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
    {
        await AddBearerToken();
        var leaveType = await _client.LeaveTypesGETAsync(id);
        return _mapper.Map<LeaveTypeVM>(leaveType);
    }

    public async Task<List<LeaveTypeVM>> GetLeaveTypes()
    {
        await AddBearerToken();
        var leavetypes = await _client.LeaveTypesAllAsync();

        return _mapper.Map<List<LeaveTypeVM>>(leavetypes);
    }

    public async Task<Response<Guid>> UpdateLeaveType(LeaveTypeVM leaveTypevm, int id)
    {
        try
        {
            await AddBearerToken();
            var updateLeavetypeCommand = _mapper.Map<UpdateLeaveTypeCommand>(leaveTypevm);
            await _client.LeaveTypesPUTAsync(id.ToString(), updateLeavetypeCommand);
            return new Response<Guid>()
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

}
