namespace TextAdventure.Classes
{
    public static class ScreenWriter
    {
        public static void ConsoleWriteLine(string text, int delay = 10)
        {
            foreach (Char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);

            }
            Console.Write("\n");
        }
        public static void ConsoleWrite(string text, int delay = 10)
        {
            foreach (Char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);

            }
        }
    }
}
