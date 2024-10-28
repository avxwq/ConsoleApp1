using Spectre.Console;

class View
{
    private Table table;
    private Panel ActionPanel;

    private Layout RootLayout;
    private Layout TopBar;
    private Layout BottomView;
    private Layout RightBottomView;
    private Layout ActionLayout;
    private Layout HelpLayout;
    private Layout ProductLayout;

    NavBar TopNavBar = new NavBar();


    private static bool shouldUpdate = true;
    static string lastKeyPressed = "None";

    static int i =0;

    public View(ProductTable productTable)
    {
        this.table = productTable.GetTable();

        RootLayout = new Layout();
        TopBar = new Layout();
        BottomView = new Layout();
        RightBottomView = new Layout();
        ActionLayout = new Layout();
        HelpLayout = new Layout();
        ProductLayout = new Layout();

        generateLayout();

        RenderWindow();
    }

    private void generateLayout()
    {

        Panel productView = new Panel(table.Alignment(Justify.Left)).Border(BoxBorder.None);

        ActionPanel = new Panel("Hello").Expand();

        TopBar.Update(TopNavBar.GetTable().Width(150).Alignment(Justify.Center).Expand());

        RootLayout.SplitRows(TopBar, BottomView);

        ProductLayout.Update(productView);

        RightBottomView.SplitRows(ActionLayout, HelpLayout);

        BottomView.SplitColumns(ProductLayout, RightBottomView);


        HelpLayout.Update(new Help("[bold blue]Use arrows to navigate[/]\n[bold yellow]Press S to search[/]\n[bold red]Press Q to quit[/]").GetPanel().Border(BoxBorder.Rounded));

        ActionLayout.Update(ActionPanel);

        TopBar.Size(5);

        var cursor = AnsiConsole.Cursor;
        
        Task.Run(() => RenderWindow());

        cursor.Hide();

        Listen();

    }

    private void UpdateLayout(string Key)
    {
        var navbar = TopNavBar.GetTable();
        if (Key == "RightArrow")
        {
            navbar.UpdateCell(0, i, "[bold red]Sklep[/]");
            i++;
        }
    }

    private async void RenderWindow()
    {
        await AnsiConsole.Live(RootLayout)
            .StartAsync(async ctx =>
            {
                while (true)
                {
                    if (shouldUpdate)
                    {
                        shouldUpdate = false;
                        UpdateLayout(lastKeyPressed);
                        ctx.Refresh();
                        await Task.Delay(25);
                    }
                }
            });
    }

    public static string Listen()
    {
        while (true)
        {

            var key = Console.ReadKey(intercept: true).Key;

            lastKeyPressed = key.ToString();

            shouldUpdate = true;
        }
    }
}

class NavBar
{
    private Table navTable = new Table();
    public NavBar()
    {
       
        navTable.AddColumn("");
        navTable.AddColumn("");
        navTable.AddColumn("");
        navTable.AddColumn("");
        navTable.HideHeaders();
        navTable.AddRow("Sklep", "Kategorie", "Koszyk", "Profil");

    }

    public Table GetTable()
    {
        return navTable;   
    }
}

class Help
{
    Panel HelpPanel;

    public Help(string panelText)
    {
        HelpPanel = new Panel(panelText);
    }
    public Panel GetPanel()
    { 
        return HelpPanel;
    }
}