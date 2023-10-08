namespace TextAdventure.Classes
{
    public class Room : BaseObject
    {
        public List<int> ItemsById { get; set; }
        public List<Door> Doors { get; set; } = new List<Door>();
        public Room(string name, string description, string detailedDescription,
                    List<int> items)
        {
            Name = name;
            Description = description;
            DetailedDescription = detailedDescription;
            ItemsById = items;
        }

        public void AddDoors(List<Door> doors)
        {
            Doors = doors;
        }

        public List<Item> GetItemsInRoom()
        {
            List<Item> items = Item.AllItems;
            List<Item> result = new();
            foreach (Item item in items)
            {
                if (ItemsById.Contains(item.Id))
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
