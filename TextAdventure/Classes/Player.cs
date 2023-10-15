using System.Runtime.CompilerServices;

namespace TextAdventure.Classes
{
    public class Player : BaseObject
    {
        public List<Item> Inventory { get; set; }
        public static Player ThePlayer { get; set; }

        public Player(string name)
        {
            Name = name[0..1].ToUpper() + name[1..].ToLower();
            Inventory = new List<Item>();
            Description = "It's you";
            DetailedDescription = "Your clothes are ripped and covered in blood... What happened...";
            ThePlayer = this;
        }
        public void PickUpItem(Item item)
        {
            Inventory.Add(item);
        }
        public void DropItem(Item item)
        {
            if (Inventory.Contains(item))
            {
                Inventory.Remove(item);
            }
        }
        public static Player GetPlayer()
        {
            return ThePlayer;
        }
    }
}
