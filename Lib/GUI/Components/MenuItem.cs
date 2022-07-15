using Project.Lib.GUI;

namespace Project.Lib.GUI.Components {
    public class MenuItem : Menu {
        public string? text;
        public bool active;
        public GuiColors highlightColor;
        public GuiColors textColor;
        public MenuItemBehaviors behavior;
        public int _id { get; set; }
        public int position;

        public MenuItem SetItemText(string itemText) {
            text = itemText;
            return this;
        }

        public MenuItem SetActiveState(bool activeState) {
            active = activeState;
            return this;
        }

        public MenuItem SetHighlightColor(GuiColors color) {
            highlightColor = color;
            return this;
        }

        public MenuItem SetTextColor(GuiColors color) {
            textColor = color;
            return this;
        }

        public MenuItem SetItemPosition(int itemPosition) {
            position = itemPosition;
            return this;
        }
    }
}