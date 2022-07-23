namespace DuckSales.Domains.Products.Entities;

public class Produto
{
    public Guid Id { get; set; }

    public string Nome { get; set; }

    public string Departamento { get; set; }

    public int QuantidadeEmEstoque { get; set; }

    public decimal PrecoUnitario { get; set; }
}
