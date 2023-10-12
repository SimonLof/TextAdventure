using TextAdventure.Classes.EffectClasses;

namespace TextAdventure.Classes
{
    public static class InputHandler
    {
        #region Game input handle
        public static void GetOutcome(string inputString, ref Player player, ref Map map, ref bool running)
        {
            try
            {
                while (inputString.Contains("  "))
                {
                    inputString = inputString.Trim().Replace("  ", " ");
                }
                List<string> inputCommands = inputString.Split(' ').ToList();
                int numberOfCommands = inputCommands.Count;
                switch (inputCommands[0].ToLower())
                {
                    case "q":
                        ScreenWriter.ConsoleWrite("Are you sure?(y/n) ", 0);
                        if (Console.ReadLine().ToLower().Trim() == "y")
                        {
                            running = false;
                        }
                        else
                            ScreenWriter.ConsoleWriteLine("Aborted quit.", 0);
                        break;
                    case "use":
                        if (numberOfCommands > 1)
                        {
                            foreach (Item item in player.Inventory)
                            {
                                if (item.Name.ToLower() == inputCommands[1])
                                {
                                    try
                                    {
                                        foreach (Effect effect in item.ItemEffects)
                                        {
                                            effect.DoEffect();
                                        }
                                        break;
                                    }
                                    catch
                                    {
                                        ScreenWriter.ConsoleWriteLine("Can't use " + inputCommands[1]);
                                    }
                                }
                            }
                        }
                        break;
                    case "go":
                        if (numberOfCommands > 1)
                        {
                            switch (inputCommands[1].ToLower())
                            {
                                case "north":
                                    LookForTheDoor(map, Facing.North);
                                    break;
                                case "east":
                                    LookForTheDoor(map, Facing.East);
                                    break;
                                case "south":
                                    LookForTheDoor(map, Facing.South);
                                    break;
                                case "west":
                                    LookForTheDoor(map, Facing.West);
                                    break;
                                default:
                                    ScreenWriter.ConsoleWriteLine($"Can't go {inputCommands[1]}.");
                                    break;
                            }
                        }
                        else
                            ScreenWriter.ConsoleWriteLine("Go where?");
                        break;
                    case "pick":
                        if (inputCommands.Count > 1)
                        {
                            switch (inputCommands[1].ToLower())
                            {
                                case "up":
                                    if (inputCommands.Count > 2)
                                    {
                                        int[] itemIds = map.CurrentRoom.ItemsById.ToArray();
                                        List<Item> roomItems = Item.AllItems.Where(i => itemIds.Contains(i.Id)).ToList();
                                        if (roomItems.Count > 0)
                                        {
                                            bool itemFound = false;
                                            foreach (Item item in roomItems)
                                            {
                                                if (item.Name.ToLower() == inputCommands[2].ToLower())
                                                {
                                                    player.PickUpItem(item);
                                                    map.CurrentRoom.ItemsById.Remove(item.Id);
                                                    ScreenWriter.ConsoleWriteLine($"You picked up {item.Name}.");
                                                    itemFound = true;
                                                    break;
                                                }
                                            }
                                            if (!itemFound)
                                                ScreenWriter.ConsoleWriteLine($"No item named {inputCommands[2]} in this room.");
                                        }
                                    }
                                    else
                                    {
                                        ScreenWriter.ConsoleWriteLine("Pick up what?");
                                    }
                                    break;
                                case "nose":
                                    ScreenWriter.ConsoleWriteLine("Gross.");
                                    break;
                                case "ass":
                                    ScreenWriter.ConsoleWriteLine("You like it.");
                                    break;
                                case "bellybutton":
                                    ScreenWriter.ConsoleWriteLine("A sense of serenity washes over you...");
                                    break;
                                default:
                                    ScreenWriter.ConsoleWriteLine("Pick what?");
                                    break;
                            }
                        }
                        else
                            ScreenWriter.ConsoleWriteLine("Pick what?");
                        break;
                    case "bag":
                        if (player.Inventory.Count > 0)
                        {
                            foreach (Item item in player.Inventory)
                                ScreenWriter.ConsoleWriteLine(item.Name + " - " + item.Description);
                        }
                        else
                            ScreenWriter.ConsoleWriteLine("You bag is empty.");
                        break;
                    case "look":
                        if (numberOfCommands > 1)
                        {
                            if (inputCommands[1].ToLower() == "around")
                            {
                                ScreenWriter.ConsoleWriteLine("You see...", 150);
                                foreach (Door door in map.CurrentRoom.Doors)
                                {
                                    ScreenWriter.ConsoleWriteLine("A door facing " + door.Direction.ToString());
                                }
                                ScreenWriter.ConsoleWriteLine(map.CurrentRoom.Name + " - " + map.CurrentRoom.DetailedDescription);
                                List<Item> items = map.CurrentRoom.GetItemsInRoom();
                                if (items.Count > 0)
                                {
                                    ScreenWriter.ConsoleWriteLine("You also see some items...");
                                    foreach (Item item in items)
                                    {
                                        ScreenWriter.ConsoleWriteLine(item.Name + " - " + item.Description);
                                    }
                                }
                                else
                                {
                                    ScreenWriter.ConsoleWriteLine("No items in this room.");
                                }
                            }
                            else if (inputCommands[1].ToLower() == "at")
                            {
                                if (inputCommands.Count > 2)
                                {
                                    //int[] itemIds = map.CurrentRoom.ItemsById.ToArray();
                                    BaseObject lookingAt = player.Inventory.SingleOrDefault(i => i.Name.ToLower().Equals(inputCommands[2].ToLower())) ??
                                                           map.CurrentRoom.GetItemsInRoom().SingleOrDefault(i => i.Name.ToLower().Equals(inputCommands[2].ToLower()));
                                    if (lookingAt == null)
                                    { lookingAt = map.CurrentRoom.Doors.FirstOrDefault(d => d.Name.ToLower() == inputCommands[2].ToLower()); }
                                    if (lookingAt == null)
                                    { lookingAt = "room" == inputCommands[2].ToLower() ? map.CurrentRoom : null; }
                                    if (lookingAt == null)
                                    { ScreenWriter.ConsoleWriteLine("Nothing here named " + inputCommands[2] + "."); }
                                    else
                                    { ScreenWriter.ConsoleWriteLine(lookingAt.Name + " - " + lookingAt.DetailedDescription); }
                                }
                                else
                                {
                                    ScreenWriter.ConsoleWriteLine("What do you want to look at?");
                                }
                            }
                            else
                            {
                                ScreenWriter.ConsoleWriteLine("Can't look " + inputCommands[1] + ".");
                            }
                        }
                        else
                        {
                            ScreenWriter.ConsoleWriteLine("Look?!? Look what?!");
                        }
                        break;
                    default:
                        throw new Exception("I don't know what you mean by that...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static bool LookForTheDoor(Map map, Facing facing)
        {
            if (map.CurrentRoom.Doors.Any(d => d.Direction == facing))
            {
                if (!map.CurrentRoom.Doors.Where(d => d.Direction == facing).SingleOrDefault().Locked)
                {
                    ScreenWriter.ConsoleWriteLine("Going " + facing.ToString().ToLower() + "...", 100);
                    map.CurrentRoom = map.CurrentRoom.Doors.Where(d => d.Direction == facing).SingleOrDefault().LeadsToo;
                    ScreenWriter.ConsoleWriteLine(map.CurrentRoom.Description);
                    return true;
                }
                else
                {
                    ScreenWriter.ConsoleWriteLine("The door is locked!");
                    return false;
                }
            }
            else
            {
                ScreenWriter.ConsoleWriteLine("No door that way.");
                return false;
            }
        }
        #endregion

        #region Creator Mode
        #region Creator mode
        public static void CreatorMode()
        { // put this in its own class?
            Map map = new Map();
            FileHandler.GetAllItems(map); ;
            string userInput = "";
            while (userInput != "q")
            {
                Console.WriteLine("Add (r)oom or add (i)tem. Map will be generated from the rooms. (a)ll items and their index, for room construction.");
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "r":
                        Console.WriteLine("\"<Name>,<Description>,<Detailed Description>,<item1>§<item2>§<item3>\" if no items just typ 999");
                        string makeRoom = Console.ReadLine();
                        if (makeRoom != "")
                        {
                            try
                            {
                                List<string> roomProps = makeRoom.Split(',').ToList();
                                List<string> roomItemIds = roomProps[3].Split('§').ToList();
                                Room room = new(roomProps[0], roomProps[1], roomProps[2], roomItemIds.Select(i => int.Parse(i)).ToList());
                                FileHandler.AddRoomToFile(room);
                                Console.WriteLine(room.Name + " added.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        break;
                    case "i":
                        Console.WriteLine("\"<Name>,<Description>,<Detailed Description>\" Id is automatic, item-interaction are its own thing.");
                        string makeItem = Console.ReadLine();
                        if (makeItem != "")
                        {
                            try
                            {
                                List<string> itemProps = makeItem.Split(',').ToList();
                                Item item = new(itemProps[0], itemProps[1], itemProps[2]);
                                FileHandler.AddItemToFile(item);
                                Console.WriteLine(item.Name + " added.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        break;
                    case "a":
                        try
                        {
                            foreach (Item item in Item.AllItems)
                            {
                                ScreenWriter.ConsoleWriteLine(item.Id + " : " + item.Name + " : " + item.Description, 0);
                            }
                            break;
                        }
                        catch { break; }
                }
            }
            ScreenWriter.ConsoleWrite("Quitting creator mode");
            ScreenWriter.ConsoleWriteLine(".......", 250);
            Console.WriteLine("Restart the app to make sure added things get loaded correctly.");
        }
        #endregion
    }
}
