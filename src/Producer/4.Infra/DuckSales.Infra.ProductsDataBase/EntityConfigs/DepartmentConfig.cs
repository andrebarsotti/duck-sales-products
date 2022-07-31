namespace DuckSales.Infra.ProductsDataBase.EntityConfigs;

public class DepartmentConfig : IEntityTypeConfiguration<Departament>
{
    public void Configure(EntityTypeBuilder<Departament> builder)
    {
        builder.ToTable("tb_department");

        builder.HasKey(ent => ent.Id);

        builder.Property(ent => ent.Id).ValueGeneratedOnAdd()
                                       .HasValueGenerator<SequentialGuidValueGenerator>()
                                       .HasColumnName("department_id")
                                       .IsRequired(true);
        builder.Property(ent => ent.Name).HasColumnName("department_name")
                                         .HasMaxLength(Departament.MaxNameSize)
                                         .IsRequired(true);
    }
}
