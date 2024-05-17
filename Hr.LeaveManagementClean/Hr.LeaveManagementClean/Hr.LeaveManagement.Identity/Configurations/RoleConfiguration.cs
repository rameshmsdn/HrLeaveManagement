using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Identity.Configurations;

public class RoleConfiguration: IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData
            (
                new IdentityRole
                {
                    Id = "cac43a6b-f7bb-4448-baaf-1add431ccbbf",
                    Name = "Employee",
                    NormalizedName = "Employee"
                },
                new IdentityRole
                {
                    Id = "cbc43abg-f7bb-4443-baaf-1add431ccbbf",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }

            );
    }
}
