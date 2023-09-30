namespace TextAdventure.Classes
{
    public class Player
    {
        // Player class, hold all items, should be the center of all actions.
        public string Name { get; set; }
        public List<Item> Inventory { get; set; }
        public Coordinates Coords { get; set; }

        public Player(string name)
        {
            Name = name;
            Inventory = new List<Item>();
            Coords = new Coordinates(0,0);
        }
        public Player(string name, Coordinates coords)
        {
            Name = name;
            Inventory = new List<Item>();
            Coords = coords;
        }
        public Player(string name, int x, int y) 
        {
            Name = name;
            Inventory = new List<Item>();
            Coords = new Coordinates(x, y);
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
