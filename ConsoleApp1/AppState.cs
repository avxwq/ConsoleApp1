using Spectre.Console;


public enum CurrView
{
    Produkty,
    Kategorie,
    Koszyk,
    Profil
}

class AppState
{
    CurrView CurrentView;

    Table navbar;
    Layout RootLayout;

    private static bool shouldUpdate = true;
    static string lastKeyPressed = "None";

    static int i = 0;

    public AppState(NavBar topNavBar, Layout RootLayout)
    {
        this.navbar = topNavBar.GetTable();
        this.RootLayout = RootLayout;
        CurrentView = CurrView.Produkty;
    }
    public void UpdateWindow(string Key)
    {
        switch(CurrentView)
        {
            case CurrView.Produkty:
                {

                }break;
            
            case CurrView.Kategorie:
                {

                }break;

            case CurrView.Koszyk:
                {

                }break;

            case CurrView.Profil:
                {

                }break;
        }
    }

    public async void RenderWindow()
    {
        await AnsiConsole.Live(RootLayout)
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

    public string Listen()
    {
        while (true)
        {

            var key = Console.ReadKey(intercept: true).Key;

            lastKeyPressed = key.ToString();

            shouldUpdate = true;
        }
    }
}

