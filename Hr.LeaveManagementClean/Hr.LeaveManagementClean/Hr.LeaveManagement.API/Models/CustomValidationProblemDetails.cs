using Microsoft.AspNetCore.Mvc;

namespace Hr.LeaveManagement.API.Models;

public class CustomValidationProblemDetails: ProblemDetails
{
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}
