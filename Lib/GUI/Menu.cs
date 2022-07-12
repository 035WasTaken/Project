using Project.Lib.GUI;

namespace Project.Lib.GUI
{
    public class Menu
    {
        /// <summary>
        /// Public property used to designate the MenuType of this Menu. This determines how the Menu is rendered.
        /// </summary>
        public MenuTypes menuType;

        /// <summary>
        /// Protected property used to designate whether this Menu is shown or not. A hidden Menu still exists, but is not rendered.
        /// </summary>
        protected bool hidden = true;

        /// <summary>
        /// Protected delegate for Menu events
        /// </summary>
        protected delegate void MenuEvent(MenuItem item);

        /// <summary>
        /// Protected MenuEvent that fires whenever a MenuItem is selected
        /// </summary>
        protected event MenuEvent? OnSelect;

        /// <summary>
        /// Protected MenuEvent that fires whenever a MenuItem is highlighted
        /// </summary>
        protected event MenuEvent? OnHighlight;

        /// <summary>
        /// Protected MenuEvent that fires whenever this Menu is rendered
        /// </summary>
        protected event MenuEvent? OnRender;

        /// <summary>
        /// Protected MenuEvent that fires whenever this Menu is updated
        /// </summary>
        protected event MenuEvent? OnUpdate;

        /// <summary>
        /// Private property used to store and keep track of each MenuItem in this Menu.
        /// </summary>
        private List<MenuItem> menuItems = new List<MenuItem>();

        /// <summary>
        /// Private property used to assign incremental IDs to MenuItems.
        /// </summary>
        private int itemCount = 0;

        /// <summary>
        /// Method used to render the Menu initially. Call this when you want to show the Menu for the first time, or need to modify it's structure.
        /// </summary>
        public void Render() {
            foreach(MenuItem item in menuItems) {
                Console.WriteLine(item.text);
            }
        }

        /// <summary>
        /// Method used to update the Menu. Call this when you need to update visuals without modifying the structure.
        /// </summary>
        public void Update() {

        }

        /// <summary>
        /// Show the Menu
        /// </summary>
        public void Show() {
            hidden = false;
        }

        /// <summary>
        /// Hide the Menu
        /// </summary>
        public void Hide() {
            hidden = true;
        }

        /// <summary>
        /// Add a single MenuItem to the Menu
        /// </summary>
        public void AddItem(MenuItem menuItem) {
            menuItem._id = itemCount++;
            menuItems.Add(menuItem);
        }

        /// <summary>
        /// Add a single MenuItem to the Menu
        /// </summary>
        public void AddItem(string itemText, GuiColors itemHighlightColor = GuiColors.White, GuiColors itemTextColor = GuiColors.White, bool itemActive = true) {
            MenuItem item = new MenuItem(itemText, itemHighlightColor, itemTextColor, itemActive);
            item._id = itemCount++;
            menuItems.Add(item);
        }

        /// <summary>
        /// Add multiple MenuItems at a time
        /// </summary>
        public void AddItems(List<MenuItem> items) {
            foreach(MenuItem item in items) {
                item._id = itemCount++;
                menuItems.Add(item);
            }
        }
    }
}