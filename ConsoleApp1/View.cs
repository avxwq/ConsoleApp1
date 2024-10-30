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

        cursor.Hide();

        state = new AppState(TopNavBar, RootLayout);

        Task.Run(() => state.RenderWindow());


        state.Listen();

    }
}
