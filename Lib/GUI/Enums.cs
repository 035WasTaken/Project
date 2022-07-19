public enum MenuTypes {
    List,
    Graph,
    Row,
    Column,
};

public enum MenuItemBehaviors {
    OpenMenu,
    CloseMenu,
}

public enum GuiColors {
    White,
    Red,
    Green,
    Blue,
    Yellow,
    Cyan,
    Purple,
};

[Flags]
public enum MenuKeyCode {
    Return = 0x0D,
    Escape = 0x1B,
    Left = 0x25,
    Up, 
    Right,
    Down,
}