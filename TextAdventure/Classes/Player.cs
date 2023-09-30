using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Classes
{
    public class Player
    {
        public string Name { get; set; }
        public List<Item> Inventory { get; set; }

        public Player(string name)
        {
            Name = name;
            Inventory = new List<Item>();
        }
        public void PickUpItem(Item item)
        {
            Inventory.Add(item);
        }
        public void DropItem(Item item) 
        {  
            Inventory.Remove(item); 
        }
    }
}
