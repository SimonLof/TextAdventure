using TextAdventure.Classes;
using TextAdventure.Classes.EffectClasses;

internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length != 0)
            if (args[0] == "-c") { CreationMode.CreateStuff(); }
        bool running = true;
        #region Initial Stuff
        Console.CursorVisible = false;
        running = StupidIntro(running);
        if (!running) { Environment.Exit(0); }
        FancyIntro();
        Console.CursorVisible = true;
        string name = string.Empty;
        do
        {
            ScreenWriter.ConsoleWrite("What is your name? ");
            name = Console.ReadLine();
        } while (name.Trim().Length < 1);
        Player player = new(name);
        ScreenWriter.ConsoleWriteLine("....", 250);
        Map map = GameSetUp();
        FileHandler.GetInteractions();

        Console.Clear();
        bool firstLook = true;
        #endregion
        #region Main Loop
        while (running)
        {
            try
            {
                if (firstLook)
                {
                    Console.WriteLine("'help' for a list of commands.");
                    ScreenWriter.ConsoleWriteLine(map.CurrentRoom.Description);
                    firstLook = false;
                }
                string userInput = Console.ReadLine();
                if (userInput == null || userInput.Trim() == "")
                {
                    ScreenWriter.ConsoleWrite("Please say something. 'help' for list of commands. ");
                    continue;
                }
                InputHandler.GetOutcome(userInput, ref player, ref map, ref running);
            }
            catch (Exception ex)
            {
                ScreenWriter.ConsoleWriteLine(ex.Message);
            }
        }
        #endregion
    }

    private static bool StupidIntro(bool running)
    {
        string wargames = string.Empty;
        while (wargames != "y" && wargames != "yes")
        {
            ScreenWriter.ConsoleWrite("Shall we play a game? ");
            Console.CursorVisible = true;
            wargames = Console.ReadLine().ToLower().Trim();
            if (wargames is "gtw" or "global thermonuclear war")
            {
                CreationMode.CreateStuff();
            }
            else if (wargames is "no" or "n")
            {
                running = false;
                break;
            }
            Console.CursorVisible = false;
        }
        return running;
    }

    private static void FancyIntro()
    {
        Console.Clear();
        Thread.Sleep(1000);
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Red;
        string introText1 = "....What a horrible night....";
        string introText2 = "....To have a curse....";
        Console.SetCursorPosition((Console.WindowWidth / 2) - introText1.Length / 2, Console.WindowHeight / 3);
        ScreenWriter.ConsoleWrite(introText1, 20, true);
        Thread.Sleep(2000);
        Console.SetCursorPosition((Console.WindowWidth / 2) - introText2.Length / 2, (Console.WindowHeight / 2));
        ScreenWriter.ConsoleWrite(introText2, 40, true);
        Thread.Sleep(2000);
        LightningEffect lightningEffect = new();
        lightningEffect.DoEffect();
        Thread.Sleep(1000);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    #region Setup and Map Logic
    public static Map GameSetUp()
    {
        Map map = new();
        Random random = new(Environment.TickCount);
        List<Room> rooms = FileHandler.GetRooms();
        Facing previousRoom = Facing.South;
        rooms[0].AddDoors(new List<Door>() { new Door(previousRoom, rooms[1]) });
        for (int i = 1; i < rooms.Count - 1; i++)
        {
            Facing excludedDirection = InvertFacing(previousRoom);
            rooms[i].Doors.Add(new(excludedDirection, rooms[i - 1]));
            do
            {
                previousRoom = (Facing)random.Next(4);
            } while (previousRoom == excludedDirection);
            rooms[i].Doors.Add(new(previousRoom, rooms[i + 1]));
        }
        rooms[^2].Doors[^1].Locked = true; // Lock the door to the final room.
        rooms[^1].AddDoors(new() { new(InvertFacing(previousRoom), rooms[^2]) }); // Unnecessary door, you win when you enter?
        foreach (Room room in rooms)
        {
            map.AddRoom(room);
        }
        map.CurrentRoom = map.MapLayout[0];
        FileHandler.GetItems();
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
    #endregion
}