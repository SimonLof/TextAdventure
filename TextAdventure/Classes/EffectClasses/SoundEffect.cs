namespace TextAdventure.Classes.EffectClasses
{
    public class SoundEffect : Effect
    {
        public int Frequency { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; } = "The frequency of the beep, ranging from 37 to 32767 hertz.\n" +
                                                  "The duration of the beep is measured in milliseconds.";

        public SoundEffect(int frequency = 200, int duration = 200)
        {
            Frequency = frequency;
            Duration = duration;
        }

        [System.Runtime.Versioning.SupportedOSPlatform("windows")]
        public override void DoEffect()
        {
            Console.Beep(Frequency, Duration);
        }
    }
}
