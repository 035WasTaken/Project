using Project.Lib;
using System.Linq;
using Project.Lib.GUI.Components;


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
        /// Protected property used to keep track of the current selected line
        /// </summary>
        protected int currentLine = 0;

        /// <summary>
        /// Protected delegate for Menu events
        /// </summary>
        protected delegate void MenuEvent(MenuItem? item = null);

        /// <summary>
        /// Protected delegate for keypresses when a menu is active
        /// </summary>
        protected delegate void MenuKeyEvent(ConsoleKeyInfo cki);
        
        /// <summary>
        /// Protected MenuEvent that fires whenever a key is pressed down.
        /// </summary>
        protected event MenuKeyEvent? OnKeyDown;

        /// <summary>
        /// Protected MenuEvent that fires whenever a MenuItem is selected
        /// </summary>
        protected event MenuEvent? OnSelect;

        /// <summary>
        /// Protected MenuEvent that fires whenever a MenuItem is deselected
        /// <summary>
        protected event MenuEvent? OnDeselect;

        /// <summary>
        /// Protected MenuEvent that fires whenever a MenuItem is highlighted
        /// </summary>
        protected event MenuEvent? OnHighlight;

        /// <summary>
        /// Protected MenuEvent that fires whenever a MenuItem is unhighlighted
        protected event MenuEvent? OnHighlightFinish;

        /// <summary>
        /// Protected MenuEvent that fires whenever this Menu is rendered
        /// </summary>
        protected event MenuEvent? OnRender;

        /// <summary>
        /// Protected MenuEvent that fires whenever this Menu is updated
        /// </summary>
        protected event MenuEvent? OnUpdate;

        /// <summary>
        /// Private property used to control when keypresses are monitored 
        /// </summary>
        protected bool listenForKeypress = false;

        /// <summary>
        /// Protected property used to keep track of the raw menu text
        /// </summary?
        protected string _displayText = "";

        /// <summary>
        /// Private property used to store and keep track of each MenuItem in this Menu.
        /// </summary>
        private List<MenuItem> menuItems = new List<MenuItem>();

        /// <summary>
        /// Private property used to assign incremental IDs to MenuItems.
        /// </summary>
        private int itemCount = 0;

        private void SetCurrentLineHighlightColor(GuiColors color) {
            
        }

        private void SetCurrentLineTextColor(GuiColors color) {

        }

        private void SetHighlightColor(GuiColors color) {

        }

        private void SetTextColor(GuiColors color) {

        }

        /// <summary>
        /// Starts a new thread and listens for keypresses on that thread so as to not block the main thread
        /// </summary>
        public void MonitorKeypress() {
            
            var loopWorker = new Thread(() => { 
                do {

                    if(Console.KeyAvailable) {
                        OnKeyDown?.Invoke(Console.ReadKey(true));
                    }
                

                } while(listenForKeypress);
            });

            OnKeyDown += h_OnKeyDown;
            loopWorker.Start();

        }
        /// <summary>
        /// Hook for handling the OnKeyDown event
        /// </summary>
        public virtual void h_OnKeyDown(ConsoleKeyInfo cki) {
            switch(cki.Key) {
                case ConsoleKey.UpArrow:

                break;
            }

            //Update();
        }

        /// <summary>
        /// Hook for handling the OnHighlight event
        /// </summary>
        public virtual void h_OnHighlight(MenuItem? item) {
            if(item != null) {
                item.text = $"\x1b[;30;47{item?.text}\x1b[0m";
            }
        }

        /// <summary>
        /// Method used to render the Menu initially. Call this when you want to show the Menu for the first time, or need to modify it's structure.
        /// </summary>
        public void Render() {
            string text = "";
            foreach(MenuItem item in menuItems) {
                text += item.text + "\n";
            }

            Console.Clear();
            Console.WriteLine(text);
            listenForKeypress = true;
        }

        /// <summary>
        /// Method used to update the Menu. Call this when you need to update visuals without modifying the structure.
        /// </summary>
        public void Update(MenuItem itemChanged) {

        }

        /// <summary>
        /// Show the Menu
        /// </summary>
        public void Show() {
            hidden = false;
            listenForKeypress = true;
        }

        /// <summary>
        /// Hide the Menu
        /// </summary>
        public void Hide() {
            hidden = true;
            listenForKeypress = false;
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
            MenuItem item = new MenuItem()
            .SetItemText(itemText)
            .SetHighlightColor(itemHighlightColor)
            .SetTextColor(itemTextColor)
            .SetActiveState(itemActive);

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