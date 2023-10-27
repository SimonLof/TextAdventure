namespace TextAdventure.Classes.EffectClasses
{
    public class WinTheGameEffect : Effect
    {
        public WinTheGameEffect()
        {
            Name = "win";
        }

        public override void DoEffect()
        {
            Console.Clear();
            Thread.Sleep(1000);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;

            string outroText1 = "....You feel yourself losing control....";
            string outroText2 = "....Everything fades to black....";
            string outroText3 = "....What is happening?....";
            Console.SetCursorPosition((Console.WindowWidth / 2) - outroText1.Length / 2, Console.WindowHeight / 3);
            ScreenWriter.ConsoleWrite(outroText1, 100, true);
            Thread.Sleep(2000);
            Console.SetCursorPosition((Console.WindowWidth / 2) - outroText2.Length / 2, (Console.WindowHeight / 2));
            ScreenWriter.ConsoleWrite(outroText2, 150, true);
            Thread.Sleep(2000);
            Console.SetCursorPosition((Console.WindowWidth / 2) - outroText3.Length / 2, (Console.WindowHeight / 3) * 2);
            ScreenWriter.ConsoleWrite(outroText3, 200, true);
            Thread.Sleep(2000);

            LightningEffect lightningEffect = new(2, 100, ConsoleColor.DarkRed);
            lightningEffect.DoEffect();
            Thread.Sleep(1000);
            Console.Clear();
            Thread.Sleep(1000);

            string finalText1 = "Game Over!";
            string finalText2 = "Press enter to quit.";
            Console.SetCursorPosition((Console.WindowWidth / 2) - finalText1.Length / 2, Console.WindowHeight / 3);
            Console.ForegroundColor = ConsoleColor.Green;
            ScreenWriter.ConsoleWrite(finalText1, 10, true);
            Thread.Sleep(1000);
            Console.SetCursorPosition((Console.WindowWidth / 2) - finalText2.Length / 2, Console.WindowHeight / 2);
            ScreenWriter.ConsoleWrite(finalText2, 10, true);
            Song.PlayMario();
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}