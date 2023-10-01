namespace TextAdventure.Classes
{
    public class InputHandler
    {
        public void GetOutcome(string inputString, ref Player player, ref Map map, ref Room room)
        {
            if (inputString[..2].ToLower() == "go ".ToLower())
            {
                switch (inputString[2..].ToLower())
                {
                    case "north":
                        // make breakout method to do these checks.
                        if (map.MapLayout.ContainsKey(new Coordinates(player.Coords.X, player.Coords.Y + 1)) &&
                            map.MapLayout[player.Coords].Doors.Any(d => d.Direction == Facing.North))
                        {
                            player.Coords.Y += 1;
                        }
                        else
                            Console.WriteLine("Can't go north.");
                        break;
                    case "east":
                        if (map.MapLayout.ContainsKey(new Coordinates(player.Coords.X + 1, player.Coords.Y)) &&
                            map.MapLayout[player.Coords].Doors.Any(d => d.Direction == Facing.East))
                        {
                            player.Coords.X += 1;
                        }
                        else
                            Console.WriteLine("Can't go east.");
                        break;
                    case "south":
                        if (map.MapLayout.ContainsKey(new Coordinates(player.Coords.X, player.Coords.Y - 1)) &&
                            map.MapLayout[player.Coords].Doors.Any(d => d.Direction == Facing.South))
                        {
                            player.Coords.Y -= 1;
                        }
                        else
                            Console.WriteLine("Can't go south.");
                        break;
                    case "west":
                        if (map.MapLayout.ContainsKey(new Coordinates(player.Coords.X-1, player.Coords.Y)) &&
                            map.MapLayout[player.Coords].Doors.Any(d => d.Direction == Facing.West))
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

        }
    }
}
