using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace TextAdventure.Classes
{
    public static class InputHandler
    {
        #region FirstOutcomeHandler
        public static void GetOutcome(string inputString, ref Player player, ref Map map, ref bool running)
        {
            if (inputString.ToLower().Trim() == "q")
            {
                while (true)
                {
                    Console.Write("Are you sure? (y/n)");
                    string quitQuestion;
                    try { quitQuestion = Console.ReadLine().ToLower().Trim(); }
                    catch { continue; }
                    if (quitQuestion == "y")
                    {
                        running = false;
                        Console.WriteLine("Quitting...");
                        break;
                    }
                    else if (quitQuestion == "n")
                    {
                        Console.WriteLine("Aborted quit.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Enter \"y\" or \"n\"");
                    }
                }
            }
            else if (inputString.Length > 1 && inputString[..2].ToLower() == "go".ToLower())
            {
                if (inputString.Length > 3)
                {
                    switch (inputString[3..].ToLower().Trim())
                    {
                        case "north":
                            // make breakout method to do these checks.
                            if (CheckDirection(new Coordinates(player.Coords.X, player.Coords.Y + 1), map, player, Facing.North))
                            {
                                player.Coords.Y += 1;
                            }
                            else
                                Console.WriteLine("Can't go north.");
                            break;
                        case "east":
                            if (CheckDirection(new Coordinates(player.Coords.X + 1, player.Coords.Y), map, player, Facing.East))
                            {
                                player.Coords.X += 1;
                            }
                            else
                                Console.WriteLine("Can't go east.");
                            break;
                        case "south":
                            if (CheckDirection(new Coordinates(player.Coords.X, player.Coords.Y - 1), map, player, Facing.South))
                            {
                                player.Coords.Y -= 1;
                            }
                            else
                                Console.WriteLine("Can't go south.");
                            break;
                        case "west":
                            if (CheckDirection(new Coordinates(player.Coords.X - 1, player.Coords.Y), map, player, Facing.West))
                            {
                                player.Coords.X -= 1;
                            }
                            else
                                Console.WriteLine("Can't go west.");
                            break;
                        default:
                            Console.WriteLine("Where do you want to go?!?");
                            break;
                    }
                }
                else
                    Console.WriteLine("Where do you want to go?!?");
            }
            else if (inputString.ToLower().Trim() == "inv" || inputString.ToLower().Trim() == "inventory")
            {
                foreach (Item item in player.Inventory)
                {
                    Console.WriteLine(item.Name + ": " + item.Description + "\n" + item.DetailedDescription);
                }
            } // Split this up, to remove the first 4 or 8 chars in the string for item name check.
            else if (inputString.ToLower()[..4] == "get " || inputString.ToLower()[..8] == "pick up ")
            {
                Room room = map.GetRoomFromCoords(player.Coords);
                Item? getItem = room.Items.SingleOrDefault(i => i.Name.ToLower().Trim().Contains(inputString.ToLower().Trim()[4..]));
                if (getItem != null)
                {
                    player.Inventory.Add(getItem);
                    map.GetRoomFromCoords(player.Coords).Items.Remove(getItem);
                }
            }
            else if (inputString.ToLower().Trim() == "look around")
            {
                Console.WriteLine(map.GetRoomFromCoords(player.Coords).Description);
            }
            else
                throw new Exception();
        }
        #endregion
        private static bool CheckDirection(Coordinates coords, Map map, Player player, Facing facing)
        {
            // Check that the map has a room with the new coordinates and that there's a door towards it in the current room.
            // Remove map check and do a only a door facing check? Map logic should guarantee doors lead to rooms.
            if (map.MapLayout.ContainsKey(coords) &&
                map.MapLayout[player.Coords].Doors.Any(d => d.Direction == facing))
            {
                return true;
            }
            return false;
        }
        #region new input handler with .split(' ')
        // New version of inputhandling
        public static void GetOutcomeTest(string inputString, ref Player player, ref Map map, ref bool running)
        {
            List<string> inputCommands = inputString.Split(' ').ToList();
            int numberOfCommands = inputCommands.Count;
            switch (inputCommands[0].ToLower())
            {
                case "q":
                    Console.Write("Are you sure?(y/n) ");
                    if (Console.ReadLine().ToString().ToLower().Trim() == "y")
                    {
                        running = false;
                    }
                    else
                        Console.WriteLine("Aborted quit.");
                    break;
                case "go":
                    if (numberOfCommands > 1)
                    {
                        switch (inputCommands[1].ToLower())
                        {
                            case "north": // Need method for checking door direction.
                                          // Skip room direction check?
                                          // If there's a door => should always be a room. Guaranteed by map create logic.
                                LookForTheDoor(player, map, Facing.North);
                                break;
                            case "east":
                                LookForTheDoor(player, map, Facing.East);
                                break;
                            case "south":
                                LookForTheDoor(player, map, Facing.South);
                                break;
                            case "west":
                                LookForTheDoor(player, map, Facing.West);
                                break;
                            default:
                                Console.WriteLine($"Can't go {inputCommands[1]}.");
                                break;
                        }
                    }
                    else
                        Console.WriteLine("Go where?");
                    break;
                case "pick":
                    if (inputCommands.Count > 1)
                    {
                        switch (inputCommands[1].ToLower())
                        {
                            case "up":
                                if (inputCommands.Count > 2)
                                {
                                    List<Item> roomItems = map.GetRoomFromCoords(player.Coords).Items;
                                    if (roomItems.Count > 0)
                                    {
                                        bool itemFound = false;
                                        foreach (Item item in roomItems)
                                        {
                                            if (item.Name.ToLower() == inputCommands[2].ToLower())
                                            {
                                                player.PickUpItem(item);
                                                map.GetRoomFromCoords(player.Coords).Items.Remove(item);
                                                Console.WriteLine($"You picked up {item.Name}.");
                                                itemFound = true;
                                                break;
                                            }
                                        }
                                        if (!itemFound)
                                            Console.WriteLine($"No item named {inputCommands[2]} in this room.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Pick up what?");
                                }
                                break;
                            case "nose":
                                Console.WriteLine("Gross.");
                                break;
                            case "ass":
                                Console.WriteLine("You like it.");
                                break;
                            case "bellybutton":
                                Console.WriteLine("A sense of serenity washes over you...");
                                break;
                            default:
                                Console.WriteLine("Pick what?");
                                break;
                        }
                    }
                    else
                        Console.WriteLine("Pick what?");
                    break;
                case "bag":
                    if (player.Inventory.Count > 0)
                    {
                        foreach (Item item in player.Inventory)
                            Console.WriteLine(item.Name + ": " + item.Description);
                    }
                    else
                        Console.WriteLine("You bag is empty.");
                    break;
                case "look":
                    if (numberOfCommands > 1)
                    {
                        if (inputCommands[1].ToLower() == "around")
                        {
                            Item[] items = map.GetRoomFromCoords(player.Coords).Items.ToArray();
                            if (items.Length > 0)
                            {
                                foreach (Item item in items)
                                {
                                    Console.WriteLine(item.Name + ": " + item.Description);
                                }
                            }
                            else
                            {
                                Console.WriteLine("No items in this room.");
                            }
                        }
                    }
                    break;
                default:
                    throw new Exception("I don't know what you mean by that...");
            }
        }

        private static bool LookForTheDoor(Player player, Map map, Facing facing)
        {
            if (map.GetRoomFromCoords(player.Coords).Doors.Any(d => d.Direction == facing))
            {
                Console.WriteLine(map.GetRoomFromCoords(player.Coords).Doors.SingleOrDefault(d => d.Direction == facing)?.Name);
                if (facing == Facing.North) player.Coords.Y += 1;
                if (facing == Facing.South) player.Coords.Y -= 1;
                if (facing == Facing.East) player.Coords.X += 1;
                if (facing == Facing.West) player.Coords.X -= 1;
                return true;
            }
            else
            {
                Console.WriteLine("No door that way.");
                return false;
            }
        }
        #endregion
    }
}
