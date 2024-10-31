using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class Database
{
    private readonly DatabaseContext _context;

    public Database()
    {
        _context = new DatabaseContext();
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public List<User> GetAllUsers()
    {
        return _context.Users.Include(u => u.CartList).ToList();
    }

    public User GetUserById(int id)
    {
        return _context.Users.Include(u => u.CartList).FirstOrDefault(u => u.Id == id);
    }

    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void DeleteUser(int id)
    {
        var user = GetUserById(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }

    public void AddProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public List<Product> GetAllProducts()
    {
        return _context.Products.ToList();
    }

    public Product GetProductById(int id)
    {
        return _context.Products.FirstOrDefault(p => p.Id == id);
    }

    public void UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
    }

    public void DeleteProduct(int id)
    {
        var product = GetProductById(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }

    public void AddCategory(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
    }

    public List<Category> GetAllCategories()
    {
        return _context.Categories.Include(c => c.Products).ToList();
    }

    public Category GetCategoryById(int id)
    {
        return _context.Categories.Include(c => c.Products).FirstOrDefault(c => c.Id == id);
    }

    public void UpdateCategory(Category category)
    {
        _context.Categories.Update(category);
        _context.SaveChanges();
    }

    public void DeleteCategory(int id)
    {
        var category = GetCategoryById(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }

    public void AddCart(Cart cart)
    {
        _context.Carts.Add(cart);
        _context.SaveChanges();
    }

    public List<Cart> GetAllCarts()
    {
        return _context.Carts.Include(c => c.Products).ToList();
    }

    public Cart GetCartById(int id)
    {
        return _context.Carts.Include(c => c.Products).FirstOrDefault(c => c.Id == id);
    }

    public void UpdateCart(Cart cart)
    {
        _context.Carts.Update(cart);
        _context.SaveChanges();
    }

    public void DeleteCart(int id)
    {
        var cart = GetCartById(id);
        if (cart != null)
        {
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }
    }
}

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Cart> Carts { get; set; }

    public DatabaseContext() : base() { }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=shop.db");
        }
    }
}

public class User
{
    public int Id { get; set; }  // Primary Key
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }

    // Navigation Property to Cart
    public List<Product> CartList { get; set; } = new List<Product>();

    public User() { }
}

public class Product
{
    public int Id { get; set; }  // Primary Key
    public string Name { get; set; }
    public string Category { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
}

public class Category
{
    public int Id { get; set; }  // Primary Key
    public string Name { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
}

public class Cart
{
    public int Id { get; set; }  // Primary Key
    public int UserId { get; set; }  // Foreign Key
    public List<Product> Products { get; set; } = new List<Product>();
}
