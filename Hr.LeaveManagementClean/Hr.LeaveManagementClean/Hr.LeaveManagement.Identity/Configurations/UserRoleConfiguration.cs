using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(new IdentityUserRole<string>
        {
            RoleId = "cac43a6b-f7bb-4448-baaf-1add431ccbbf",
            UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
        },
        new IdentityUserRole<string>
        {
            RoleId = "cbc43abg-f7bb-4443-baaf-1add431ccbbf",
            UserId = "9e224865-a26d-4544-a8h6-9063d048cdb9"
        });

    }
}
