namespace DuckSales.Infra.ProductsDataBase.EntityConfigs;

public class DepartmentConfig : IEntityTypeConfiguration<Departament>
{
    public void Configure(EntityTypeBuilder<Departament> builder)
    {
        builder.ToTable("tb_department");

        builder.HasKey(ent => ent.Id);

        builder.Property(ent => ent.Id)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .HasColumnName("dpt_id")
            .IsRequired();
        builder.Property(ent => ent.Name)
            .HasField("_name")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("dpt_name")
            .HasMaxLength(Departament.MaxNameSize)
            .IsRequired();
        builder.Property(ent => ent.CreatedIn)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<DateTimeUtcNowValueGenerator>()
            .HasColumnName("dpt_created_in")
            .IsRequired();
        builder.Property(ent => ent.UpdatedIn)
            .ValueGeneratedOnUpdate()
            .HasValueGenerator<DateTimeUtcNowValueGenerator>()
            .HasColumnName("dpt_updated_in")
            .IsRequired(false);
    }
}
