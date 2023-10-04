namespace TextAdventure.Classes
{
    public static class InputHandler
    {
        #region input handler using .split(' ')
        // New version of inputhandling
        public static void GetOutcome(string inputString, ref Player player, ref Map map, ref bool running)
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
