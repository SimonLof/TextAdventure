using TextAdventure.Classes;

internal class Program
{
    private static void Main(string[] args)
    {
        // Set up ------- make breakout method for setup and setup from textfiles
        // Just experimental stuff that should be replaced.
        bool running = true;
        Console.Write("Enter your name: ");
        Player player = new(name: Console.ReadLine());
        List<Item> allItems = new List<Item>();
        using (StreamReader sr = new StreamReader(@".\items.txt"))
        {
            while(!sr.EndOfStream)
            {
                string[] itemStrings = sr.ReadLine().Split(',');
                allItems.Add(new(itemStrings[0], itemStrings[1], itemStrings[2], new List<Item>()));
            }
        }
        Room otherRoom = new("Kitchen", "A damp kitchen full of mold and cockroaches.",
            "Upon further inspection you notice the cockroaches have made a small society in one of the cabins... They seem happy.", new List<Item>());
        Door door = new("Wooden door", "It's made of wood.", false, Facing.South, otherRoom);
        List<Door> doors = new()
        {
            door
        };
        Room room = new("Starting room", "A dark cellar.", "Reeks of fish and cheese.", allItems);
        room.AddDoors(doors);
        Map map = new(room);

        // Make a 'visited' prop in room and list all visited rooms and coords when looking at "map"?
        // Or try to draw a map OMEGALUL
        bool firstLook = true;
        // Main loop
        while (running)
        {
            if (firstLook)
            {
                Console.WriteLine(map.CurrentRoom.Description);
                firstLook = false;
            }
            // Input handler starting to work out!
            string userInput = Console.ReadLine();
            if (userInput == null || userInput == "") { continue; }
            try
            {
                InputHandler.GetOutcome(userInput, ref player, ref map, ref running);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}