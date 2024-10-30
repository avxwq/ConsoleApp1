using Spectre.Console;

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