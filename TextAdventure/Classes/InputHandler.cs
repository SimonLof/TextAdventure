﻿namespace TextAdventure.Classes
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
                            case "north":
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
                                    List<Item> roomItems = map.CurrentRoom.Items;
                                    if (roomItems.Count > 0)
                                    {
                                        bool itemFound = false;
                                        foreach (Item item in roomItems)
                                        {
                                            if (item.Name.ToLower() == inputCommands[2].ToLower())
                                            {
                                                player.PickUpItem(item);
                                                map.CurrentRoom.Items.Remove(item);
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
                            Console.WriteLine("You see...");
                            Thread.Sleep(1000);
                            foreach (Door door in map.CurrentRoom.Doors)
                            {
                                Console.WriteLine("A door facing " + door.Direction.ToString());
                            }
                            Thread.Sleep(1000);
                            Console.WriteLine(map.CurrentRoom.DetailedDescription);
                            Item[] items = map.CurrentRoom.Items.ToArray();
                            Thread.Sleep(1000);
                            if (items.Length > 0)
                            {
                                Console.WriteLine("You also see some items...");
                                Thread.Sleep(1000);
                                foreach (Item item in items)
                                {
                                    Console.WriteLine(item.Name + ": " + item.Description);
                                    Thread.Sleep(300);
                                }
                            }
                            else
                            {
                                Console.WriteLine("No items in this room.");
                            }
                        }
                        else if (inputCommands[1].ToLower() == "at")
                        {
                            if (inputCommands.Count > 2)
                            {
                                BaseObject lookingAt = player.Inventory.SingleOrDefault(i => i.Name.ToLower().Equals(inputCommands[2].ToLower()));
                                if (lookingAt == null)
                                { lookingAt = map.CurrentRoom.Items.SingleOrDefault(i => i.Name.ToLower().Equals(inputCommands[2].ToLower())); }
                                if (lookingAt == null)
                                { lookingAt = map.CurrentRoom.Doors.SingleOrDefault(d => d.Name.ToLower() == inputCommands[2].ToLower()); }
                                if (lookingAt == null)
                                { lookingAt = "room" == inputCommands[2].ToLower() ? map.CurrentRoom : null; }
                                if (lookingAt == null)
                                { Console.WriteLine("Nothing here named " + inputCommands[2]); }
                                else
                                { Console.WriteLine(lookingAt.Name + ": " + lookingAt.DetailedDescription); }

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
            if (map.CurrentRoom.Doors.Any(d => d.Direction == facing))
            {
                map.CurrentRoom = map.CurrentRoom.Doors.Where(d => d.Direction == facing).SingleOrDefault().LeadsToo;
                Console.WriteLine(map.CurrentRoom.Description);
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
