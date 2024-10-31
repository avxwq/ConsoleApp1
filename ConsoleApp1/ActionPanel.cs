using Spectre.Console;

class ActionPanel
{
    Panel panel;

    public ActionPanel()
    {
        this.panel = new Panel("Hello").Expand();
    }

    public Panel GetPanel()
    {
        return this.panel;
    }

    public void ShowCategories()
    {
    }
}