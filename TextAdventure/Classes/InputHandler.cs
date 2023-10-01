using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace TextAdventure.Classes
{
    public static class InputHandler
    {
        public static void GetOutcome(string inputString, ref Player player, ref Map map, ref bool running)
        {
            if (inputString.ToLower().Trim() == "q")
            {
                while (true)
                {
                    Console.Write("Are you sure? (y/n)");
                    string quitQuestion = Console.ReadLine().ToLower().Trim();
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
                Item getItem = room.Items.SingleOrDefault(i => i.Name.ToLower().Trim().Contains(inputString.ToLower().Trim()[4..]));
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
        private static bool CheckDirection(Coordinates coords, Map map, Player player, Facing facing)
        {
            // Check that the map has a room with the new coordinates and that there's a door towards it in the current room.
            if (map.MapLayout.ContainsKey(coords) &&
                map.MapLayout[player.Coords].Doors.Any(d => d.Direction == facing))
            {
                return true;
            }
            return false;
        }
    }
}
