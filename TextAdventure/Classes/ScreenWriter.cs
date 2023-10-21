namespace TextAdventure.Classes
{
    public static class ScreenWriter
    {
        public static void ConsoleWriteLine(string text, int delay = 10, bool noCursorMode = false)
        { // varför har jag gjort double negative....
            Console.CursorVisible = false;
            foreach (Char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.Write("\n");
            if (!noCursorMode)
                Console.CursorVisible = true;
        }
        public static void ConsoleWrite(string text, int delay = 10, bool noCursorMode = false)
        {
            Console.CursorVisible = false;
            foreach (Char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            if (!noCursorMode)
                Console.CursorVisible = true;
        }
    }
}
