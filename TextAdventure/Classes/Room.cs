namespace TextAdventure.Classes
{
    public class Room : BaseObject
    {
        public List<Item> Items { get; set; }
        public RoomEvent RoomEvent { get; set; }
        public List<Door> Doors { get; set; }

        public Room(string name, string description, string detailedDescription,
                    List<Item> items, RoomEvent roomEvent, List<Door> doors)
        {
            Name = name;
            Description = description;
            DetailedDescription = detailedDescription;
            Items = items;
            RoomEvent = roomEvent;
            Doors = doors;
        }
    }
}
