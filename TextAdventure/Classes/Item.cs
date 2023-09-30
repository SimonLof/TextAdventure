namespace TextAdventure.Classes
{
    public class Item
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }

        public Item(string name,  string description)
        {
            Name = name;
            Id = 0;
            Description = description;
        }
    }
}
