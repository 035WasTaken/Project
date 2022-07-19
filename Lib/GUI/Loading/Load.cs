namespace Project.Lib.GUI.Loading
{
    public class Load
    {
        static async public Task LoadResource(string message) {
            int counter = 0;
            Console.CursorVisible = false;

            Console.Write(message);

            while(true) {
                counter++;

                switch(counter % 4) {
                    case 0: Console.Write("/"); counter = 0; break;
                    case 1: Console.Write("-"); break;
                    case 2: Console.Write("\\"); break;
                    case 3: Console.Write("|"); break;
                }

                await Task.Delay(100);
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }

        }
    }
}