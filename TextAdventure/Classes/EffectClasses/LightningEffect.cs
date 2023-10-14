namespace TextAdventure.Classes.EffectClasses
{
    public class LightningEffect : Effect
    {
        public int Repetitions { get; set; }
        public int DelayInMs { get; set; }
        public ConsoleColor ColorOfLightning { get; set; }
        public LightningEffect(int repetitions = 3, int delayInMs = 100, ConsoleColor color = ConsoleColor.DarkRed)
        {
            Name = "lightning";
            Repetitions = repetitions;
            DelayInMs = delayInMs;
            ColorOfLightning = color;
        }
        public override void DoEffect()
        {
            ConsoleColor original = Console.BackgroundColor;
            for (int i = 0; i < Repetitions; i++)
            {
                Console.BackgroundColor = ColorOfLightning;
                Console.Clear();
                Thread.Sleep(DelayInMs / 2);
                Console.BackgroundColor = original;
                Console.Clear();
                Thread.Sleep(DelayInMs);
            }
        }
    }
}
