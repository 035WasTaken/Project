using Project.Lib.GUI;
using Project.Lib.GUI.Loading;
using Project.Lib.GUI.Components;

Menu mainMenu = new Menu();
mainMenu.AddItem(new MenuItem().SetItemText("Start"));
mainMenu.AddItem(new MenuItem().SetItemText("Options"));
mainMenu.AddItem(new MenuItem().SetItemText("Exit"));
mainMenu.Render();

mainMenu.MonitorKeypress();