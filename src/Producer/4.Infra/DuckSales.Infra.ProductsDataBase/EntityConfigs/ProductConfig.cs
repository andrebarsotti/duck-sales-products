using Microsoft.EntityFrameworkCore.Metadata;

namespace DuckSales.Infra.ProductsDataBase.EntityConfigs;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("tb_products");

        builder.HasKey(ent => ent.Id);

        builder.Property(ent => ent.Id)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .HasColumnName("prd_id")
            .IsRequired();

        builder.Property(ent => ent.Name)
            .HasField("_name")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("prd_name")
            .HasMaxLength(Product.MaxNameSize)
            .IsRequired();

        builder.Property(ent => ent.QuantityAvaiableInStock)
            .HasField("_quantityAvaiableInStock")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("prd_qtd_avaiable_stock")
            .IsRequired();

        builder.Property(ent => ent.UnitPrice)
            .HasField("_unitprice")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("prd_prc_unit")
            .HasPrecision(8, 2)
            .IsRequired();

        builder.Property(ent => ent.CreatedIn)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<DateTimeUtcNowValueGenerator>()
            .HasColumnName("prd_created_in")
            .IsRequired();
        builder.Property(ent => ent.UpdatedIn)
            .ValueGeneratedOnUpdate()
            .HasValueGenerator<UpdateDateTimeUtcNowValuGenerator>()
            .HasColumnName("prd_updated_in")
            .IsRequired(false);

        builder.Property<Guid?>("dpt_id").IsRequired(false);

        builder.HasOne(ent => ent.Departament)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull)
            .HasForeignKey("dpt_id")
            .IsRequired();

        builder.Navigation(ent => ent.Departament)
            .HasField("_department")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
