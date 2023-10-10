namespace TextAdventure.Classes
{
    public static class FileHandler
    {
        private static readonly string RoomsFilePath = ".\\rooms.txt";
        private static readonly string ItemsFilePath = ".\\items.txt";
        private static readonly string MapFilePath = ".\\map.txt"; // Save current map to continue playing? Later project
        private static StreamReader? reader;
        private static StreamWriter? writer;
        public static List<Room> GetRooms()
        {
            List<Room> rooms = new();
            try
            {
                if (File.Exists(RoomsFilePath))
                {
                    using (reader = new(RoomsFilePath))
                    {
                        while (!reader.EndOfStream)
                        {
                            List<string> roomProps = reader.ReadLine().Split(",").ToList();
                            List<string> itemIds = roomProps[3].Split("§").ToList();
                            Room room = new(roomProps[0], roomProps[1], roomProps[2], itemIds.Select(i => int.Parse(i)).ToList());
                            rooms.Add(room);
                        }
                    }
                }
                else
                {
                    AddRoomToFile(new Room("Staring room", "A small closet. You don't know how you got here.", "It's very crammed.", new() { 0, 1 }));
                    AddRoomToFile(new Room("Kitchen", "A small kitchen. Empty pots and pans everywhere.", "You see some cockroaches who seem to be building a nest... strange.", new() { 2, 3 }));
                    rooms = GetRooms();
                }
                return rooms;
            }
            catch { return rooms; }
        }

        public static List<Item> GetAllItems()
        {
            List<Item> items = new();
            try
            {
                using (reader = new(ItemsFilePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string[] line = reader.ReadLine().Split(',');
                        Item item = new(line[0], line[1], line[2]);
                        items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return items;
        }
        public static void AddItemToFile(Item item)
        {
            try
            {
                using (writer = new(ItemsFilePath, true))
                {
                    writer.WriteLine(item.Name + "," + item.Description + "," + item.DetailedDescription);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        public static void AddRoomToFile(Room room)
        {
            try
            {
                string roomItemIds = string.Empty;
                string firstRoom = "";
                room.ItemsById.ForEach(i => roomItemIds += i.ToString() + "§");
                roomItemIds = roomItemIds.Remove(roomItemIds.Length - 1);
                if (File.Exists(RoomsFilePath)) firstRoom = "\n";
                using (writer = new(RoomsFilePath, true))
                {
                    writer.Write(firstRoom + room.Name + "," + room.Description + "," + room.DetailedDescription + "," + roomItemIds);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
