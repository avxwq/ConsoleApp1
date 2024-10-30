using Spectre.Console;

class NavBar
{
    private Table navTable = new Table();
    public NavBar()
    {
       
        navTable.AddColumn("Sklep");
        navTable.AddColumn("Kategorie");
        navTable.AddColumn("Koszyk");
        navTable.AddColumn("Profil");
       // navTable.HideHeaders();
       // navTable.AddRow("Sklep", "Kategorie", "Koszyk", "Profil");

    }

    public Table GetTable()
    {
        return navTable;   
    }
}
