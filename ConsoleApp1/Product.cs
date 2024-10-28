public class Product
{
    private int id;
    private string name;
    private string category;
    private int price;
    private int quantity;
    private string description;

    public Product(int id, string name, string category, int price, int quantity, string description)
    {
        this.id = id;
        this.name = name;
        this.category = category;
        this.price = price;
        this.quantity = quantity;
        this.description = description;
    }

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Name
    {
        get { return name; }
        set
        {
            name = value;
        }
    }
    public string Category
    {
        get { return category; }
        set { category = value; }
    }
    public int Price
    {
        get { return price; }
        set { price = value; }
    }
    public int Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

}