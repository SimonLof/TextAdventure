namespace TextAdventure.Classes
{
    public class Item : BaseObject
    {
        public static List<Item> AllItems { get; set; } = new List<Item>();
        private static int _id = 0;

        public List<Item> CanBeUsedWith { get; set; }
        public int Id { get; }

        public Item(string name, string description, string detailedDescription)
        {
            this.Name = name;
            this.Description = description;
            this.DetailedDescription = detailedDescription;
            this.CanBeUsedWith = new();
            Id = _id;
            _id++;
            AllItems.Add(this);
        }
        // New class that just lists interactions?
        public Item(string name, string description, string detailedDescription, List<Item> canBeUsedWith)
        {
            Name = name;
            this.Description = description;
            this.DetailedDescription = detailedDescription;
            this.CanBeUsedWith = canBeUsedWith;
            Id = _id;
            _id++;
            AllItems.Add(this);
        }
    }
}
