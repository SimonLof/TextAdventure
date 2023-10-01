using TextAdventure.Classes;

internal class Program
{
    private static void Main(string[] args)
    {
        // Set up ------- make breakout method for setup
        // Just experimental stuff that should be replaced.
        bool running = true;
        Console.Write("Enter your name: ");
        Player player = new(name: Console.ReadLine());
        Item myItem = new("Key", "Old rusty key", "You think it might open the exit door...", new List<Item>());
        Item myItem2 = new("Sword", "A sword for slashing.", "A little dull, a little rusty.", new List<Item>());
        List<Item> items = new()
        {
            myItem,
            myItem2
        };
        Door door = new("Wooden door", "It's made of wood.", false, Facing.South);
        List<Door> doors = new()
        {
            door
        };
        RoomEvent roomEvent = new("Nothing.", "Nothing special here.");

        Map map = new();
        Room room = new("Starting room", "A dark cellar.", "Reaks of fish and cheese.", items, roomEvent, doors);
        map.AddRoom(new(0, 0), room);


        // Main loop
        while (running)
        {
            // Input handler starting to work out!
            string userInput = Console.ReadLine();
            if (userInput == null || userInput == ""){ continue; }
            try
            {
                InputHandler.GetOutcome(userInput, ref player, ref map, ref running);

            }
            catch (Exception ex)
            {
                Console.WriteLine("I don't know what you mean by that...");
            }
        }
    }
}