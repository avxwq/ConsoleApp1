using Spectre.Console;

public class ProductTable
{
    Table table;

    public ProductTable()
    {
        table = new Table().Border(TableBorder.Minimal);
        createTopBar(["Nazwa", "Kategoria", "Cena", "Ilość", "Opis"]);
    }

    public void createTopBar(List<string> cells)
    {
        foreach (var cell in cells)
        {
            table.AddColumn(cell);
        }

    }

    public void AddProduct(Product product)
    {
        string[] itemProperties = new string[5];
        itemProperties[0] = product.Name;
        itemProperties[1] = product.Category;
        itemProperties[2] = product.Price.ToString();
        itemProperties[3] = product.Quantity.ToString();
        itemProperties[4] = product.Description;
        table.AddRow(itemProperties);

    }
    public Table GetTable()
    {
        return this.table;
    }
}