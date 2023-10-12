﻿using System.Runtime.CompilerServices;

namespace TextAdventure.Classes
{
    public class Player
    {
        // Player class, hold all items, should be the center of all actions.
        public string Name { get; set; }
        public List<Item> Inventory { get; set; }
        public static Player ThePlayer { get; set; }

        public Player(string name)
        {
            Name = name;
            Inventory = new List<Item>();
            ThePlayer = this;
        }
        public void PickUpItem(Item item)
        {
            Inventory.Add(item);
        }
        public void DropItem(Item item)
        {
            //Inventory[Inventory.IndexOf(item)] = null;
            Inventory.Remove(item);
        }
        public static Player GetPlayer()
        {
            return ThePlayer;
        }
    }
}
