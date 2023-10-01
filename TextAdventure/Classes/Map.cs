namespace TextAdventure.Classes
{
    public class Map
    {
        public Dictionary<Coordinates, Room> MapLayout { get; set; }

        public Map()
        {
            MapLayout = new Dictionary<Coordinates, Room>();
        }
        public Room GetRoomFromCoords(Coordinates coords)
        {
            return MapLayout.Where(c => c.Key.X == coords.X && c.Key.Y == coords.Y).SingleOrDefault().Value;
        }

        public bool AddRoom(Coordinates coords, Room room)
        {
            // If there is already a room at gives coordinates, returns false
            // Otherwise adds the room to the coordinates and returns true
            if (MapLayout.Any(o => o.Key == coords))
            {
                return false;
            }
            else
            {
                MapLayout.Add(coords, room);
                return true;
            }
        }
        public bool RemoveRoom(Coordinates coords)
        {
            if (MapLayout.TryGetValue(coords, out Room? value) && value.Name != "End")
            {
                MapLayout.Remove(coords);
                return true;
            }
            else { return false; }
        }
    }
}
