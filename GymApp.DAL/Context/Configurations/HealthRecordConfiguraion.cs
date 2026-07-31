namespace GymApp.DAL.Context.Configurations;

internal class HealthRecordConfiguraion : IEntityTypeConfiguration<HealthRecord>
{
    public void Configure(EntityTypeBuilder<HealthRecord> builder)
    {
        builder.Property(hr=>hr.height)
            .HasColumnType("Decimal (10, 2)")
            .IsRequired();

        builder.Property(hr => hr.weight)
            .HasColumnType("Decimal (10, 2)")
            .IsRequired();

        builder.Property(hr => hr.bloodType)
            .HasColumnType("char")
            .HasMaxLength(5);
        builder.Property(Hr=> Hr.Note)
            .HasMaxLength(200);

    }
}
    