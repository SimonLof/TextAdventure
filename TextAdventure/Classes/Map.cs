namespace TextAdventure.Classes
{
    public class Map
    {
        public List<Room> MapLayout { get; set; }
        public Room CurrentRoom { get; set; }
        public static Map TheMap { get; set; }

        public Map()
        {
            MapLayout = new List<Room>();
            TheMap = this;
        }
        public void AddRoom(Room room)
        {
            MapLayout.Add(room);
        }
        public bool RemoveRoom(Room room)
        {
            int len = MapLayout.Count;
            MapLayout = MapLayout.Where(r => r.Name != room.Name).ToList();
            if (len != MapLayout.Count)
            { return true; }
            else
            { return false; }
        }
    }
}
