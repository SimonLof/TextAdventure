namespace TextAdventure.Classes
{
    public enum Facing
    {
        North,
        West,
        South,
        East
    }
    public class Door : BaseObject
    {
        public Facing Direction { get; set; }
        public bool Locked { get; set; }
        public Room LeadsToo { get; set; }

        public Door(string description, string detailedDescription, bool locked, Facing facing, Room leadsToo)
        {
            Name = "Door";
            Description = description;
            DetailedDescription = detailedDescription;
            Direction = facing;
            Locked = locked;
            LeadsToo = leadsToo;
        }

        public void Unlock() { Locked = false; }
    }
}
