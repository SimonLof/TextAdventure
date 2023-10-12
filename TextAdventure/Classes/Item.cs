using TextAdventure.Classes.EffectClasses;

namespace TextAdventure.Classes
{
    public class Item : BaseObject
    {
        public static List<Item> AllItems { get; set; } = new List<Item>();
        private static int _id = 0;
        public int Id { get; }
        public List<Effect> ItemEffects { get; set; }

        public Item(string name, string description, string detailedDescription)
        {
            this.Name = name;
            this.Description = description;
            this.DetailedDescription = detailedDescription;
            this.ItemEffects = new List<Effect>();
            Id = _id;
            _id++;
            AllItems.Add(this);
        }
    }
}
