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

        public Door(Facing facing, Room leadsToo, bool locked = false)
        {
            Name = "Door";
            Description = "Wood door";
            DetailedDescription = "It's a normal door, what do you want?";
            Direction = facing;
            Locked = locked;
            LeadsToo = leadsToo;
        }

        public void Unlock() { Locked = false; }
    }
}
