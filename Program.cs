using Project.Lib.GUI;
using Project.Lib.GUI.Components;

Menu mainMenu = new MainMenu();
mainMenu.AddItem(new MenuItem().SetItemText("Start"));
mainMenu.AddItem(new MenuItem().SetItemText("Options"));
mainMenu.AddItem(new MenuItem().SetItemText("Exit"));
mainMenu.Render();

mainMenu.MonitorKeypress();