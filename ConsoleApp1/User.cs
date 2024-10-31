class User
{
    public string Name { get; set; }

    public string Surname { get; set; }
    public string Password { get; set; }

    public string Email { get; set; }

    public List<Product> CartList { get; set; }

    public string Status { get; set; }

    public User(string name, string surname, string password, string email, List<Product> cartList, string status)
    {
        Name = name;
        Surname = surname;
        Password = password;
        Email = email;
        CartList = cartList;
        Status = status;
    }
}