using Project.Lib.GUI;

namespace Project.Lib {
    /// <summary>
    /// Currently unused class to get keypresses from the keyboard
    /// </summary>
    public static class NativeKeyboard {
        private const int KeyPressed = 0x8000;

        /// <summary>
        /// Checks if a given key is held down
        /// </summary>
        public static bool IsKeyDown(MenuKeyCode key) {
            return (GetKeyState((int)key) & KeyPressed) != 0;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetKeyState(int key);
    }
}