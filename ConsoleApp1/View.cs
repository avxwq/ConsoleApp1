using Spectre.Console;
using Spectre.Console.Extensions;
using Spectre.Console.Rendering;
using System;
using System.Xml;

class View
{
    private AppState state;
    private Table table = new Table();
    private Panel ActionPanel;

    private Layout RootLayout;
    private Layout TopBar;
    private Layout BottomView;
    private Layout RightBottomView;
    private Layout ActionLayout;
    private Layout HelpLayout;
    private Layout StartLayout;

    NavBar TopNavBar = new NavBar();

    public View()
    {

        RootLayout = new Layout();
        TopBar = new Layout();
        BottomView = new Layout();
        RightBottomView = new Layout();
        ActionLayout = new Layout();
        HelpLayout = new Layout();
        StartLayout = new Layout();

        generateLayout();
    }

    private void generateLayout()
    {
        var StartingView = new Panel("[bold]Witaj w aplikacji sklepowej![/]\n\n"
                                        + "[green]Nawiguj po aplikacji za pomocą strzałek.[/]\n"
                                        + "Aby przejść do wybranej sekcji, naciśnij [bold]Enter[/].\n\n"
                                        + "Dostępne sekcje:\n"
                                        + "- Produkty: Przeglądaj dostępne rośliny\n"
                                        + "- Kategorie: Znajdź produkty według kategorii\n"
                                        + "- Koszyk: Zobacz zawartość koszyka\n"
                                        + "- Profil: Zobacz swój profil i szczegóły konta")
                             .Header("[bold blue]Nawigacja[/]", Justify.Left);
        // Inicjalizacja ActionPanel
        ActionPanel action = new ActionPanel(RootLayout);

        // Ustawienie TopBar z nazwą "Navbar"
        TopBar.Update(TopNavBar.GetTable().Width(150).Alignment(Justify.Center).Expand());
        TopBar.Name = "Navbar"; // Nazwa wymagana, by AppState mogło znaleźć i aktualizować Navbar

        // Główna struktura RootLayout
        RootLayout.SplitRows(
            TopBar, // dodanie TopBar jako Navbar
            BottomView
        );

        // Przypisanie nazw i zaktualizowanie zawartości layoutów dla widoków
        StartLayout.Update(StartingView);
        StartLayout.Name = "Content"; // Nazwa wymagana dla widoków, aby AppState mogło aktualizować zawartość
        RightBottomView.SplitRows(ActionLayout, HelpLayout);
        BottomView.SplitColumns(StartLayout, RightBottomView);

        // Konfiguracja panelu pomocy i działania
        HelpLayout.Update(Align.Right(
            new Help("[bold blue]Use arrows to navigate[/]\n[bold yellow]Press Enter to select[/]")
            .GetPanel().Border(BoxBorder.Rounded)).BottomAligned());
        ActionLayout.Name = "Action";
        ActionLayout.Update(action.GetPanel());

        TopBar.Size(5);

        // Ukrycie kursora i uruchomienie stanu aplikacji
        var cursor = AnsiConsole.Cursor;
        cursor.Hide();

        // Inicjalizacja AppState z odpowiednią konfiguracją RootLayout
        state = new AppState(TopNavBar, RootLayout, action);

        // Uruchomienie renderowania i nasłuchiwania
        Task.Run(() => state.RenderWindow());
        state.Listen();
    }
}
