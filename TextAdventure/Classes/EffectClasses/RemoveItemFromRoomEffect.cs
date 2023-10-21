namespace TextAdventure.Classes.EffectClasses
{
    public class RemoveItemFromRoomEffect : Effect
    {
        public int ItemToRemove { get; set; }
        public RemoveItemFromRoomEffect(int itemToRemove)
        {
            Name = "remove_item_room";
            ItemToRemove = itemToRemove;
        }
        public override void DoEffect()
        {
            try
            {
                Map.TheMap.CurrentRoom.ItemsById.Remove(ItemToRemove);
            }
            catch (Exception ex)
            {
                FileHandler.LogError(ex, "Remove item from room effect error.");
            }
        }
    }
}
