namespace TextAdventure.Classes
{
    public class Item
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string  DetailedDescription { get; set; } = string.Empty;
        private static int _idCount = 0;

        public Item(string name,  string description, string detailed)
        {
            Name = name;
            Id = _idCount;
            _idCount++;
            Description = description;
            DetailedDescription = detailed;
        }

        public virtual bool UseItem(Item item)
        {
            if (item == null) return false;
            return true;
        }
    }
}
