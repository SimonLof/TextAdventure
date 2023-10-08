namespace TextAdventure.Classes
{
    public class Item : BaseObject
    {
        public List<Item> CanBeUsedWith { get; set; }

        public Item(string name, string description, string detailedDescription)
        {
            this.Name = name;
            this.Description = description;
            this.DetailedDescription = detailedDescription;
            this.CanBeUsedWith = new();
        }
        public Item(string name, string description, string detailedDescription, List<Item> canBeUsedWith)
        {
            Name = name;
            this.Description = description;
            this.DetailedDescription = detailedDescription;
            this.CanBeUsedWith = canBeUsedWith;
        }


        public virtual void ThisHappensWhenUsed() { }

        public virtual void ThisHappensWhenUsedOn() { }
    }
}
