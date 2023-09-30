using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Classes
{
    public enum Facing
    {
        North,
        West,
        South,
        East
    }
    public class Door:BaseObject
    {
        public Facing Direction { get; set; }
        public bool Locked { get; set; }

        public Door(string description, string detailedDescription, bool locked, Facing facing)
        {
            Name = "Door";
            Description = description;
            DetailedDescription = detailedDescription;
            Direction = facing;
            Locked = locked;
        }

        public void Unlock() { Locked = false; }
    }
}
