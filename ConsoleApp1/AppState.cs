using Spectre.Console;


public enum CurrView
{
    Nawigacja,
    Produkty,
    Kategorie,
    Koszyk,
    Profil
}

class AppState
{
    private CurrView currentView;
    private Layout rootLayout;
    private Table navbar;
    private static bool shouldUpdate = true;
    private static string lastKeyPressed = "None";
    private static int selectedIndex = 0;

    public AppState(NavBar topNavBar, Layout rootLayout)
    {
        this.navbar = topNavBar.GetTable();
        this.rootLayout = rootLayout;
        currentView = CurrView.Nawigacja;
        UpdateNavbar();
    }

    public void UpdateWindow(string key)
    {
        if (key == "RightArrow" || key == "LeftArrow")
        {
            selectedIndex = key == "RightArrow" ? (selectedIndex + 1) % 5 : (selectedIndex - 1 + 5) % 5;
            UpdateNavbar();
        }
        else if (key == "Enter")
        {
            currentView = (CurrView)selectedIndex;
            ShowCurrentView();
        }
    }

    private void UpdateNavbar()
    {
        var updatedNavTable = new Table();
        updatedNavTable.AddColumn(new TableColumn(selectedIndex == 0 ? "[bold red]Nawigacja[/]" : "Nawigacja").Centered());
        updatedNavTable.AddColumn(new TableColumn(selectedIndex == 1 ? "[bold red]Produkty[/]" : "Produkty").Centered());
        updatedNavTable.AddColumn(new TableColumn(selectedIndex == 2 ? "[bold red]Kategorie[/]" : "Kategorie").Centered());
        updatedNavTable.AddColumn(new TableColumn(selectedIndex == 3 ? "[bold red]Koszyk[/]" : "Koszyk").Centered());
        updatedNavTable.AddColumn(new TableColumn(selectedIndex == 4 ? "[bold red]Profil[/]" : "Profil").Centered());

        rootLayout["Navbar"].Update(updatedNavTable);
    }

    private void ShowCurrentView()
    {
        var content = currentView switch
        {
            CurrView.Nawigacja => CreateNavigationView(),
            CurrView.Produkty => new Panel("Widok: Produkty - Lista dostępnych roślin."),
            CurrView.Kategorie => new Panel("Widok: Kategorie - Przeglądaj kategorie roślin."),
            CurrView.Koszyk => new Panel("Widok: Koszyk - Zawartość twojego koszyka."),
            CurrView.Profil => CreateProfileView(),
            _ => new Panel("Witaj w aplikacji!")
        };

        content.Border(BoxBorder.Rounded).Expand();
        rootLayout["Content"].Update(content);
    }

    private Panel CreateNavigationView()
    {
        var navInstructions = new Panel("[bold]Witaj w aplikacji sklepowej![/]\n\n"
                                        + "[green]Nawiguj po aplikacji za pomocą strzałek.[/]\n"
                                        + "Aby przejść do wybranej sekcji, naciśnij [bold]Enter[/].\n\n"
                                        + "Dostępne sekcje:\n"
                                        + "- Produkty: Przeglądaj dostępne rośliny\n"
                                        + "- Kategorie: Znajdź produkty według kategorii\n"
                                        + "- Koszyk: Zobacz zawartość koszyka\n"
                                        + "- Profil: Zobacz swój profil i szczegóły konta")
                             .Header("[bold blue]Nawigacja[/]", Justify.Left);

        return navInstructions;
    }

    private Panel CreateProfileView()
    {
        var profileTable = new Table();
        profileTable.AddColumn("Profil Użytkownika");
        profileTable.AddRow($"[bold]Imię:[/] [aqua]Jan Kowalski[/]");
        profileTable.AddRow($"[bold]Email:[/] [aqua]jan.kowalski@example.com[/]");
        profileTable.AddRow($"[bold]Zakupione produkty:[/] [aqua]12[/]");
        profileTable.AddRow($"[bold]Status konta:[/] [green]Aktywny[/]");

        var actionsTable = new Table();
        actionsTable.AddColumn("[bold]Dostępne Akcje[/]");
        actionsTable.AddRow("[green]1. Zmień dane profilowe[/]");
        actionsTable.AddRow("[yellow]2. Zmień hasło[/]");
        actionsTable.AddRow("[blue]3. Przeglądaj historię zakupów[/]");
        actionsTable.AddRow("[red]4. Wyloguj się[/]");

        var profileLayout = new Layout("ProfileView")
            .SplitRows(
                new Layout("Info").Update(new Panel(profileTable).Header("[bold]Informacje[/]", Justify.Left)),
                new Layout("Actions").Update(new Panel(actionsTable).Header("[bold]Dostępne Akcje[/]", Justify.Left))
            );

        return new Panel(profileLayout).Border(BoxBorder.Rounded).Expand();
    }

    public async void RenderWindow()
    {
        await AnsiConsole.Live(rootLayout)
            .StartAsync(async ctx =>
            {
                while (true)
                {
                    if (shouldUpdate)
                    {
                        shouldUpdate = false;
                        UpdateWindow(lastKeyPressed);
                        ctx.Refresh();
                        await Task.Delay(25);
                    }
                }
            });
    }

    public void Listen()
    {
        while (true)
        {
            var key = Console.ReadKey(intercept: true).Key;
            lastKeyPressed = key.ToString();
            shouldUpdate = true;
        }
    }
}

