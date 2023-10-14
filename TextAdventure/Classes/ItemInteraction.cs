using TextAdventure.Classes.EffectClasses;

namespace TextAdventure.Classes
{
    public class ItemInteraction
    {
        public int FirstItemId { get; set; }
        public int SecondItemId { get; set; }
        public List<Effect> CombineEffects { get; set; }
        public ItemInteraction(int firstItemId, int secondItemId, List<Effect> effects)
        {
            FirstItemId = firstItemId;
            SecondItemId = secondItemId;
            CombineEffects = effects;
        }
        public void CombineEffect() 
        { 
            foreach(Effect effect in CombineEffects)
            {
                effect.DoEffect();
            }
        }
    }
}
