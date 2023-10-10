using TextAdventure.Classes;

internal class Program
{
    private static void Main(string[] args)
    {
        bool running = true;
        string wargames = string.Empty;
        ScreenWriter.ConsoleWrite("What is your name? ");
        Player player = new(name: Console.ReadLine());
        ScreenWriter.ConsoleWriteLine("....", 250);
        while (wargames.ToLower() != "y" && wargames.ToLower() != "yes")
        {
            ScreenWriter.ConsoleWrite("Shall we play a game? ");
            wargames = Console.ReadLine();
            if (wargames.ToLower() is "gtw" or "global thermonuclear war")
            {
                InputHandler.CreatorMode();
            }
            else if (wargames.ToLower() is "no" or "n")
            {
                running = false;
                break;
            }
        }
        Console.Clear();
        Map map = GameSetUp();

        bool firstLook = true;
        // Main loop
        while (running)
        {
            try
            {
                if (firstLook)
                {
                    Console.WriteLine(map.CurrentRoom.Description);
                    firstLook = false;
                }
                // Input handler starting to work out!
                string userInput = Console.ReadLine();
                if (userInput == null || userInput == "")
                {
                    ScreenWriter.ConsoleWrite("Please say something. ");
                    continue;
                }
                InputHandler.GetOutcome(userInput, ref player, ref map, ref running);
            }
            catch (Exception ex)
            {
                ScreenWriter.ConsoleWriteLine(ex.Message);
            }
        }
    }
    #region Creation and setup
    public static Map GameSetUp()
    {
        FileHandler.GetAllItems();
        Map map = new();
        Random random = new(Environment.TickCount);
        List<Room> rooms = FileHandler.GetRooms();
        Facing previousRoom = Facing.South;
        rooms[0].AddDoors(new() { new(previousRoom, rooms[1]) });
        for (int i = 1; i < rooms.Count - 1; i++)
        {
            Facing firstDoorDirection = InvertFacing(previousRoom);
            rooms[i].Doors.Add(new(firstDoorDirection, rooms[i - 1]));
            previousRoom = (Facing)random.Next(4);
            while (previousRoom == firstDoorDirection) { previousRoom = (Facing)random.Next(4); }
            rooms[i].Doors.Add(new(previousRoom, rooms[i + 1]));
        }
        rooms[^2].Doors[^1].Locked = true; // If a room has more than 2 doors I have to change this.
        rooms[^1].AddDoors(new() { new(InvertFacing(previousRoom), rooms[^2]) });
        foreach (Room room in rooms)
        {
            map.AddRoom(room);
        }
        map.CurrentRoom = map.MapLayout[0];
        return map;
    }
    private static Facing InvertFacing(Facing facing)
    {
        return facing switch
        {
            Facing.North => Facing.South,
            Facing.South => Facing.North,
            Facing.West => Facing.East,
            Facing.East => Facing.West,
            _ => Facing.North,
        };
    }
    //public static void CreatorMode()
    //{ // put this in its own class
    //    string userInput = "";
    //    while (userInput != "q")
    //    {
    //        Console.WriteLine("Add (r)oom or add (i)tem. Map will be generated from the rooms. (a)ll items and their index, for room construction.");
    //        userInput = Console.ReadLine();
    //        switch (userInput)
    //        {
    //            case "r":
    //                Console.WriteLine("\"<Name>,<Description>,<Detailed Description>,<item1>§<item2>§<item3>\" if no items just typ 999");
    //                string makeRoom = Console.ReadLine();
    //                if (makeRoom != "")
    //                {
    //                    try
    //                    {
    //                        List<string> roomProps = makeRoom.Split(',').ToList();
    //                        List<string> roomItemIds = roomProps[3].Split('§').ToList();
    //                        Room room = new(roomProps[0], roomProps[1], roomProps[2], roomItemIds.Select(i => int.Parse(i)).ToList());
    //                        FileHandler.AddRoomToFile(room);
    //                        Console.WriteLine(room.Name + " added.");
    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        Console.WriteLine(ex.Message);
    //                    }
    //                }
    //                break;
    //            case "i":
    //                // code for item creation
    //                break;
    //            case "a":
    //                try
    //                {
    //                    foreach (Item item in FileHandler.GetAllItems())
    //                    {
    //                        ScreenWriter.ConsoleWriteLine(item.Id + " : " + item.Name + " : " + item.Description, 0);
    //                    }
    //                    break;
    //                }
    //                catch { break; }
    //        }
    //    }
    //    ScreenWriter.ConsoleWrite("Quitting creator mode");
    //    ScreenWriter.ConsoleWriteLine(".......", 250);
    //}
    #endregion
}