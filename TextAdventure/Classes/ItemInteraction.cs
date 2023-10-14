using TextAdventure.Classes.EffectClasses;

namespace TextAdventure.Classes
{
    public class ItemInteraction
    {
        public static List<ItemInteraction> AllInteractions { get; set; } = new List<ItemInteraction>();
        public int FirstItemId { get; set; }
        public int SecondItemId { get; set; } // If Ids added to doors, include door Id checks in this.
        public List<Effect> CombineEffects { get; set; }
        public ItemInteraction(int firstItemId, int secondItemId, List<Effect> effects)
        {
            FirstItemId = firstItemId;
            SecondItemId = secondItemId;
            CombineEffects = effects;
            AllInteractions.Add(this);
        }
        public void CombineEffect()
        {
            foreach (Effect effect in CombineEffects)
            {
                effect.DoEffect();
            }
            ScreenWriter.ConsoleWriteLine("Successful combination! Remove this text later.");
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
