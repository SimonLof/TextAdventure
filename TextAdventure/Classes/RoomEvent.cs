namespace TextAdventure.Classes
{
    public class RoomEvent:BaseObject
    {
        public RoomEvent(string name, string description)
        {
            Name = name;
            Description = description;
            DetailedDescription = "";
        }

        public void DoEvent() { }
    }
}
