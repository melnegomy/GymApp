namespace GymApp.DAL.Context.Configurations;

internal class MemberConfiguration : GymUserConfiguration<Member>, IEntityTypeConfiguration<Member>
{
    public new void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.Property(X => X.Gender)
                   .HasConversion<string>();
        base.Configure(builder);
    }
}
