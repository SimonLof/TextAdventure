using TextAdventure.Classes.EffectClasses;

namespace TextAdventure.Classes
{
    public class Item : BaseObject
    {
        private static List<Item> AllItems { get; set; } = new List<Item>();
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

        public static void ResetItemList()
        {
            AllItems.Clear();
            _id = 0;
        }

        public static Item GetItemFromId(int id)
        {
            return AllItems.SingleOrDefault(i => i.Id == id);
        }

        public static Item GetItemFromName(string name)
        {
            return AllItems.FirstOrDefault(i => i.Name.ToLower() == name.ToLower());
        }

        public static List<Item> GetAllItems()
        {
            return AllItems;
        }
    }
}
