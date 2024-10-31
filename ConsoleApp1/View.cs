using Spectre.Console;
using Spectre.Console.Rendering;
using System;
using System.Xml;

class View
{
    private AppState state;
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
    }

    private void generateLayout()
    {
        // Tworzenie panelu widoku produktów
        Panel productView = new Panel(table.Alignment(Justify.Left)).Border(BoxBorder.None);

        // Inicjalizacja ActionPanel
        ActionPanel = new Panel("Hello").Expand();

        // Ustawienie TopBar z nazwą "Navbar"
        TopBar.Update(TopNavBar.GetTable().Width(150).Alignment(Justify.Center).Expand());
        TopBar.Name = "Navbar"; // Nazwa wymagana, by AppState mogło znaleźć i aktualizować Navbar

        // Główna struktura RootLayout
        RootLayout.SplitRows(
            TopBar, // dodanie TopBar jako Navbar
            BottomView
        );

        // Przypisanie nazw i zaktualizowanie zawartości layoutów dla widoków
        ProductLayout.Update(productView);
        ProductLayout.Name = "Content"; // Nazwa wymagana dla widoków, aby AppState mogło aktualizować zawartość
        RightBottomView.SplitRows(ActionLayout, HelpLayout);
        BottomView.SplitColumns(ProductLayout, RightBottomView);

        // Konfiguracja panelu pomocy i działania
        HelpLayout.Update(new Help("[bold blue]Use arrows to navigate[/]\n[bold yellow]Press Enter to select[/]").GetPanel().Border(BoxBorder.Rounded));
        ActionLayout.Update(ActionPanel);

        TopBar.Size(5);

        // Ukrycie kursora i uruchomienie stanu aplikacji
        var cursor = AnsiConsole.Cursor;
        cursor.Hide();

        // Inicjalizacja AppState z odpowiednią konfiguracją RootLayout
        state = new AppState(TopNavBar, RootLayout);

        // Uruchomienie renderowania i nasłuchiwania
        Task.Run(() => state.RenderWindow());
        state.Listen();
    }
}
