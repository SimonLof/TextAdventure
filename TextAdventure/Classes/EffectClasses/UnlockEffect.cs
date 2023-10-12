namespace TextAdventure.Classes.EffectClasses
{
    public class UnlockEffect : Effect
    {
        public Door Door { get; set; }
        public Map CurrentMap { get; set; }
        public UnlockEffect(Map map)
        {
            Name = "unlock";
            CurrentMap = map;
            Door = map.MapLayout[^2].Doors.FirstOrDefault(d => d.Locked);
        }
        public override void DoEffect()
        {
            if (CurrentMap.CurrentRoom.Doors.Any(d => d.Locked))
            {
                Door.Unlock();
                ShowTextEffect someText = new(Door.Name + " unlocked!");
                someText.DoEffect();
            }
        }
    }
}
