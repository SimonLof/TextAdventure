namespace TextAdventure.Classes.EffectClasses
{
    public abstract class Effect
    {
        public string Name { get; set; }
        public abstract void DoEffect();
    }
}
// Todo effects:
// 1. Color of text and background.
// 2. thread.sleep(milliseconds) but better. In seconds?
// 3. Remove/add health if I implement fightning.
// 4. Other stuff.