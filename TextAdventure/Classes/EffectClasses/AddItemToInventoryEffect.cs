namespace TextAdventure.Classes.EffectClasses
{
    public class AddItemToInventoryEffect : Effect
    {
        public int ItemToAdd { get; set; }
        public AddItemToInventoryEffect(int itemToAdd)
        {
            Name = "add_item_inv";
            ItemToAdd = itemToAdd;
        }
        public override void DoEffect()
        {
            try
            {
                Player playerToModify = Player.GetPlayer();
                playerToModify.PickUpItem(Item.GetItemFromId(ItemToAdd));
            }
            catch (Exception ex)
            {
                FileHandler.LogError(ex, "Adding item to inventory effect error.");
            }
        }

    }
}
