namespace TextAdventure.Classes.EffectClasses
{
    public class AddItemToRoomEffect : Effect
    {
        public int ItemToAdd { get; set; }

        public AddItemToRoomEffect(int itemToAdd)
        {
            Name = "add_item_room";
            ItemToAdd = itemToAdd;
        }

        public override void DoEffect()
        {
            Map.TheMap.CurrentRoom.ItemsById.Add(ItemToAdd);
        }
    }
}
