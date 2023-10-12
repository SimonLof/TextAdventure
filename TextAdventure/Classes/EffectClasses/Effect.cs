namespace TextAdventure.Classes.EffectClasses
{
    public abstract class Effect
    {
        public string Name { get; set; }
        public abstract void DoEffect();
    }
}
