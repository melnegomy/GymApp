using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp.DAL.Context.Configurations;

internal class SessionConfiguraion : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable(x =>
        {

            x.HasCheckConstraint("SessionDataCheck", "StartDate < EndDate");
            x.HasCheckConstraint("SessionCapacityCheck", "Capacity Between 1 And 25");
        });
    }
}
