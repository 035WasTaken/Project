using Project.Lib.GUI;

namespace Project.Lib.GUI
{
    public class Menu
    {
        public MenuTypes menuType;

        protected bool hidden = true;

        private List<MenuItem> menuItems = new List<MenuItem>();

        public void Render() {
            foreach(MenuItem item in menuItems) {
                Console.WriteLine(item.text);
            }
        }

        public void Update() {

        }

        public void Show() {
            hidden = false;
        }

        public void Hide() {
            hidden = true;
        }

        public void AddItem(MenuItem menuItem) {
            menuItems.Add(menuItem);
        }

        public void AddItem(string itemText, GuiColors itemHighlightColor = GuiColors.White, GuiColors itemTextColor = GuiColors.White, bool itemActive = true) {
            menuItems.Add(new MenuItem(itemText, itemHighlightColor, itemTextColor, itemActive));
        }
    }
}