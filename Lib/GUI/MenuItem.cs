using Project.Lib.GUI;

public class MenuItem : Menu {
    public string text;
    public GuiColors highlightColor;
    public GuiColors textColor;
    public bool active;

    public int _id { get; set; }

    public MenuItem(string itemText, GuiColors itemHighlightColor = GuiColors.White, GuiColors itemTextColor = GuiColors.White, bool itemActive = true) {
        text = itemText;
        highlightColor = itemHighlightColor;
        textColor = itemTextColor;
        active = itemActive;
    }
}