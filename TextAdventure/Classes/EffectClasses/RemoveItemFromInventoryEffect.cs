namespace TextAdventure.Classes.EffectClasses
{
    public class RemoveItemFromInventoryEffect : Effect
    {
        public int ItemToRemove { get; set; }

        public RemoveItemFromInventoryEffect(int itemToRemove)
        {
            Name = "remove_item_inv";
            ItemToRemove = itemToRemove;
        }
        public override void DoEffect()
        {
            try
            {
                Player.GetPlayer().DropItem(Item.AllItems.SingleOrDefault(i => i.Id == ItemToRemove));
            }
            catch (Exception ex)
            {
                FileHandler.LogError(ex);
            }
        }
    }
}
