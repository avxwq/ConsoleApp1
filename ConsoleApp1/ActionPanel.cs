using Spectre.Console;

class ActionPanel // klasa odpowiedzialna za wyswietlanie po prawej, tutaj wszystko do zmienienia
{
    Layout rootLayout;
    Panel panel;
    private static int selectedIndex = 0;

    public ActionPanel(Layout rootLayout)
    {
        this.rootLayout = rootLayout;
        this.panel = new Panel("Hello").Expand();
    }

    public Panel GetPanel()
    {
        return this.panel;
    }

    public void HandleKey(string key)
    {
        if (key == "RightArrow" || key == "LeftArrow")
        {
            selectedIndex = key == "RightArrow" ? (selectedIndex + 1) % 2 : (selectedIndex - 1 + 2) % 2;
            ShowLoginPanel();
        }
        if (key == "Escape")
        {

        }

    }

    public void ShowLoginPanel()
    {
        var chooseOption = new Table();
        chooseOption.AddColumn(new TableColumn(selectedIndex == 0 ? "[bold red]Zarejestruj sie[/]" : "Zarejestruj sie").Centered());
        chooseOption.AddColumn(new TableColumn(selectedIndex == 1 ? "[bold red]Zaloguj sie[/]" : "Zaloguj sie").Centered());

        var content = new Layout(chooseOption.Centered());
        rootLayout["Action"].Update(content);
    }
}