namespace TextAdventure.Classes
{
    public class Item : BaseObject
    {
        public static List<Item> AllItems { get; set; } = new List<Item>();
        private static int _id = 0;

        public Dictionary<int, string> CanBeUsedWith { get; set; }
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
        public Item(string name, string description, string detailedDescription, Dictionary<int, string> canBeUsedWith)
        {
            Name = name;
            this.Description = description;
            this.DetailedDescription = detailedDescription;
            this.CanBeUsedWith = canBeUsedWith;
            Id = _id;
            _id++;
            AllItems.Add(this);
        }

        //public int UseItemWith(int id, Door? door = null)   Make own class for interactions.
        //{
        //    if (CanBeUsedWith.ContainsKey(id))
        //    {
        //        if (id == 999) { door.Unlock(); } // Key can be used with item 999, can make more complex logic with door names and Ids. But only 1 locked door for now.
        //        string[] effectOfCombination = CanBeUsedWith[id].Split('§'); // index 0 is string to describe effect, index 1 is item reward from combo.
        //        ScreenWriter.ConsoleWriteLine(effectOfCombination[0]);       // Can add index 2 to indicate the removal/non-removal of used items?
        //        if (effectOfCombination.Length > 1)                          // Default to removal of items. Maybe add prop for destruction of item.
        //        {
        //            return int.Parse(effectOfCombination[1]);
        //        }
        //    }
        //    return 0;
        //}
    }
}
