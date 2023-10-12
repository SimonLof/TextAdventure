namespace TextAdventure.Classes.EffectClasses
{
    public class ShowTextEffect : Effect
    {
        public string Text { get; set; }
        public int TextDelay { get; set; }
        public ShowTextEffect(string text, int textDelay = 10)
        {
            Name = "show_text";
            Text = text;
            TextDelay = textDelay;
        }
        public override void DoEffect()
        {
            ScreenWriter.ConsoleWriteLine(Text, TextDelay);
        }
    }
}
