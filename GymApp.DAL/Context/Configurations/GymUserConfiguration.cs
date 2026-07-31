namespace GymApp.DAL.Context.Configurations;

internal abstract class GymUserConfiguration<T> : IEntityTypeConfiguration<T>where T : GymUser
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(X => X.Name)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

        builder.Property(X => X.Email)
                   .HasColumnType("varchar")
                   .HasMaxLength(100);

        builder.OwnsOne(x => x.Address, address =>
        {
            address.Property(x => x.Street)
                .HasColumnType("varchar")
                .HasMaxLength(100);
            address.Property(x => x.City)
                .HasColumnType("varchar")
                .HasMaxLength(50);
        });

        builder.Property(X => X.Phone)
            .HasColumnType("Char")
            .HasMaxLength(11);


        builder.ToTable(x => {
            x.HasCheckConstraint("Email Check", "Email LIKE '%@%.%'");
            x.HasCheckConstraint("Phone Check", "Phone LIKE '[0-9]%' AND LEN(Phone) = 11");
        });
    }
}
