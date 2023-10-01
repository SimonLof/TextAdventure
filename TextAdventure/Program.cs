using TextAdventure.Classes;

internal class Program
{
    private static void Main(string[] args)
    {
        // Just experimental stuff that should be replaced.
        Console.Write("Enter your name: ");
        Player player = new(name: Console.ReadLine());
        Item myItem = new("Key", "Old rusty key", "You think it might open the exit door...", new List<Item>());
        Item myItem2 = new("Sword", "A sword for slashing.", "A little dull, a little rusty.", new List<Item>());
        player.PickUpItem(myItem);
        player.PickUpItem(myItem2);

        foreach (Item item in player.Inventory)
        {
            Console.WriteLine(item.Name + ": " + item.Description + "\n" + item.DetailedDescription);
        }

        while (true)
        {
            // All experimental stuff that should be replaced with methods.
            string userInput = Console.ReadLine();
            Item lookedItem;
            if (userInput.ToLower() == "q") { break; }
            else if (userInput == "") { continue; }
            else if ((lookedItem = player.Inventory.FirstOrDefault(i => i.Name.ToLower() == userInput.ToLower())) != null)
            {
                Console.WriteLine("You look at " + lookedItem.Name + "... " + lookedItem.DetailedDescription);
            }
            else
            {
                Console.WriteLine("I don't know what you mean by that.");
            }
        }
    }
}