using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp.DAL.Context.Configurations;

internal class TrainerConfiguration : GymUserConfiguration<Trainer>, IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {

        builder.Property(X => X.Specialty)
                   .HasConversion<string>();

        base.Configure(builder);
    }
}
