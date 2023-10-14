using TextAdventure.Classes.EffectClasses;

namespace TextAdventure.Classes
{
    public class ItemInteraction
    {
        public static List<ItemInteraction> AllInteractions { get; set; } = new List<ItemInteraction>();
        public int FirstItemId { get; set; }
        public int SecondItemId { get; set; }
        public List<Effect> CombineEffects { get; set; } = new();
        public ItemInteraction(int firstItemId, int secondItemId)
        {
            FirstItemId = firstItemId;
            SecondItemId = secondItemId;
            AllInteractions.Add(this);
        }
        public void CombineEffect()
        {
            foreach (Effect effect in CombineEffects)
            {
                effect.DoEffect();
            }
        }
        public static bool InteractionExists(Item item1, Item item2)
        {
            if (item1.Id != item2.Id)
            {
                foreach (ItemInteraction interaction in AllInteractions)
                {
                    if (item1.Id == interaction.FirstItemId)
                    {
                        if (item2.Id == interaction.SecondItemId)
                        {
                            return true;
                        }
                    }
                    else if (item1.Id == interaction.SecondItemId)
                    {
                        if (item2.Id == interaction.FirstItemId)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
