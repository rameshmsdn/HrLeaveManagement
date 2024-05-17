using Hr.LeaveManagement.Domain.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Domain;

public class LeaveType: BaseEntity
{
    
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}
