using TextAdventure.Classes;
using TextAdventure.Classes.EffectClasses;

internal class Program
{
    private static void Main(string[] args)
    {
        bool running = true;
        #region Initial Stuff
        running = StupidIntro(running);
        FancyIntro();
        ScreenWriter.ConsoleWrite("What is your name? ");
        Player player = new(name: Console.ReadLine());
        ScreenWriter.ConsoleWriteLine("....", 250);
        Map map = GameSetUp();
        /////////////////////////// Test
        // Make combine effect file and load this with filehandler instead.
        ItemInteraction itemInteractionTest = new(6, 3);
        itemInteractionTest.CombineEffects = new List<Effect>()
        {
            new ShowTextEffect("You combined " +
                   Item.AllItems.SingleOrDefault(i => i.Id == itemInteractionTest.FirstItemId).Name + " and " +
                   Item.AllItems.SingleOrDefault(i => i.Id == itemInteractionTest.SecondItemId).Name +
                   " into a fancy wizard outfit that starts moving around on its own!\n...\nThe outfit burts into flames and leaves behind a book on the floor.",20), //Använder namnet för att hitta namnet... Kanske borde köra på Id.
            new RemoveItemFromInventoryEffect(itemInteractionTest.FirstItemId),
            new RemoveItemFromInventoryEffect(itemInteractionTest.SecondItemId),
            new AddItemToRoomEffect((Item.AllItems.SingleOrDefault(i => i.Name.ToLower() == "book")).Id)
        };
        /////////////////////////// End test
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
        while (wargames.ToLower() != "y" && wargames.ToLower() != "yes")
        {
            ScreenWriter.ConsoleWrite("Shall we play a game? ");
            wargames = Console.ReadLine();
            if (wargames.ToLower() is "gtw" or "global thermonuclear war")
            {
                CreationMode.CreateStuff();
            }
            else if (wargames.ToLower() is "no" or "n")
            {
                running = false;
                break;
            }
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
        ScreenWriter.ConsoleWrite(introText1, 20);
        Thread.Sleep(3000);
        Console.SetCursorPosition((Console.WindowWidth / 2) - introText2.Length / 2, (Console.WindowHeight / 3) * 2);
        ScreenWriter.ConsoleWrite(introText2, 40);
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
        rooms[0].AddDoors(new() { new(previousRoom, rooms[1]) });
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
        FileHandler.GetAllItems();
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