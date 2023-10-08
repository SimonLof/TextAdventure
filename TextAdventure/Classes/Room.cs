namespace TextAdventure.Classes
{
    public class Room : BaseObject
    {
        public List<Item> Items { get; set; }
        public List<Door> Doors { get; set; }

        public Room(string name, string description, string detailedDescription,
                    List<Item> items)
        {
            Name = name;
            Description = description;
            DetailedDescription = detailedDescription;
            Items = items;
            Doors = new List<Door>();
        }

        public void AddDoors(List<Door> doors)
        {
            Doors = doors;
        }
    }
}
