using System.Security.Cryptography.X509Certificates;
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
        if(player.Name.ToLower() == "god")
        {
            CreatorMode();
        }
        FileHandler.GetItems(); // Create all items
        Room otherRoom = new("Kitchen", "A damp kitchen full of mold and cockroaches.",
            "Upon further inspection you notice the cockroaches have made a small society in one of the cabins... They seem happy.", new() { 2, 3 });
        Room room = new("Starting room", "A dark cellar.", "The room reeks of fish and cheese.", new() { 0, 1 });

        // first make all the rooms, then make all the doors.
        Door rood = new("Wooden door", "It's made of wood.", false, Facing.North, room);
        Door door = new("Wooden door", "It's made of wood.", false, Facing.South, otherRoom);
        room.AddDoors(new() { door });
        otherRoom.AddDoors(new() { rood });
        Map map = new(room);
        map.AddRoom(otherRoom);

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
    public static void CreatorMode()
    {
        Console.WriteLine("Make a map.");
        while(Console.ReadLine() != "q") { }
    }
}