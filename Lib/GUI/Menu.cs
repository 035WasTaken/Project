using Project.Lib;
using System.Linq;
using Project.Lib.GUI.Components;

// need to rename everything that says GUI to UI

namespace Project.Lib.GUI
{
    /// <summary>
    /// Class for creating simple menus in the console
    /// </summary>
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

        public Menu() {
            // assign all events to their handlers in here

            OnKeyDown += h_OnKeyDown;
            OnHighlight += h_OnHighlight;
            OnHighlightFinish += h_OnHighlightFinish;
        }

        /// <summary>
        /// Starts a new thread and listens for keypresses on that thread so as to not block the main thread
        /// </summary>
        public void MonitorKeypress() {
            
            // create a new thread and assign it to the variable loopWorker
            // we do this to avoid blocking the main thread
            Thread loopWorker = new Thread(() => { 
                do {
                    
                    // check to see if a key is currently being pressed
                    if(Console.KeyAvailable) {
                        // if true, we invoke the OnKeyDown event
                        OnKeyDown?.Invoke(Console.ReadKey(true));
                    }
                
                // while the variable listenForKeypress is set to true, this loop will run
                } while(listenForKeypress);
            });

            // start the thread
            loopWorker.Start();
        }

        /// <summary>
        /// Hook for handling the OnKeyDown event
        /// </summary>
        public virtual void h_OnKeyDown(ConsoleKeyInfo cki) {
            // keep track of the last selected line in order to reset it
            int lastLine = currentLine;
            // same thing here
            MenuItem lastItem = menuItems[lastLine];

            // figure out which key was pressed
            switch(cki.Key) {
                // when the up arrow is pressed
                case ConsoleKey.UpArrow:
                    currentLine = (currentLine == 0 ? menuItems.Count - 1 : currentLine - 1);
                break;
                // when the down arrow is pressed
                case ConsoleKey.DownArrow:
                    currentLine = (currentLine == menuItems.Count - 1 ? 0 : currentLine + 1);
                break;
            }

            // we need to get the text values from the current version of the menu item
            MenuItem item = menuItems[currentLine];

            UpdateLine(0, currentLine, item.highlightedText);
            UpdateLine(0, lastLine, lastItem.text);
        }

        // if(item != null) as opposed to if(item == null) return;

        /// <summary>
        /// Hook for handling the OnHighlight event. Unused until I decide to rewrite it.
        /// </summary>
        public virtual void h_OnHighlight(MenuItem? item) {
            if(item != null) {
                item.SetItemText(item.highlightedText);
            }
        }

        /// <summary>
        /// Hook for handling the OnHighlightFinish event. Unused until I decide to rewrite it.
        public virtual void h_OnHighlightFinish(MenuItem? item) {
            if(item != null) {
                item.SetItemText(item.text);
            }
        }

        /// <summary>
        /// Method used to render the Menu initially. Call this when you want to show the Menu for the first time, or need to modify its structure.
        /// </summary>
        public void Render() {
            Console.Clear();

            foreach(MenuItem item in menuItems) {
                if(item._id == 0) {
                    Console.WriteLine($"\x1b[;30;47m{item?.text}\x1b[0m");
                    continue;
                }

                Console.WriteLine(item.text);
            }

            listenForKeypress = true;
        }

        /// <summary>
        /// Method used to update the Menu. Call this when you need to update visuals without modifying the structure.
        /// </summary>
        public void Update(MenuItem itemChanged) {

        }

        public void UpdateLine(int y) {
            // store default cursor position
            (int, int) oldCursorPosition = Console.GetCursorPosition();
            // get menu item displayed at line y
            MenuItem? item = menuItems[y];


            if(item != null) {
                // set cursor position to the beginning of line y
                Console.SetCursorPosition(0, y);
                // delete everything from the cursor to the end of the line
                Console.Write("\x1b[0k");
                // replace with updated item.text
                Console.Write(item.text);

                // reset cursor position to default
                Console.SetCursorPosition(oldCursorPosition.Item1, oldCursorPosition.Item2);
            }
        }

        public void UpdateLine(int x, int y, string text) {
            // store default cursor position
            (int, int) oldCursorPosition = Console.GetCursorPosition();
            // get menu item displayed at line y
            MenuItem? item = menuItems[y];

            if(item != null) {
                // set cursor position to beginning of line y
                Console.SetCursorPosition(0, y);
                // delete everything from the cursor to the end of the line
                Console.Write("\x1b[0k");
                // replace with updated text
                Console.Write(text);

                // reset cursor position to default
                Console.SetCursorPosition(oldCursorPosition.Item1, oldCursorPosition.Item2);   
            }
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