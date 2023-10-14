namespace TextAdventure.Classes.EffectClasses
{
    public class UnlockEffect : Effect
    {
        public Door Door { get; set; }
        public UnlockEffect()
        {
            Name = "unlock";
            Door = Map.TheMap.MapLayout[^2].Doors.FirstOrDefault(d => d.Locked);
        }
        public override void DoEffect()
        {
            Map map = Map.TheMap;
            if (map.CurrentRoom.Doors.Any(d => d.Locked)) // Only 1 locked door right now, so this check is sufficient. Add Id's for more possible keys and doors.
            {
                Door.Unlock();
                ShowTextEffect someText = new(Door.Name + " unlocked!");
                someText.DoEffect();
            }
            else
            {
                ShowTextEffect text = new("Nothing to unlock in here.");
                text.DoEffect();
            }
        }
    }
}
