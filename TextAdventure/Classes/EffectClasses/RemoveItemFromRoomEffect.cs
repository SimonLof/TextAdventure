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
            Map.TheMap.CurrentRoom.ItemsById.Remove(ItemToRemove);
        }
    }
}
