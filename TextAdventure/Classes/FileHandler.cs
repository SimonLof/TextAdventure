namespace TextAdventure.Classes
{
    public static class FileHandler
    {
        private static string RoomsFilePath = ".\\rooms.txt";
        private static string ItemsFilePath = ".\\items.txt";
        private static string MapFilePath = ".\\map.txt"; // Save current map to continue playing? Later project
        private static StreamReader? reader;
        private static StreamWriter? writer;

        public static List<Room> GetRooms()
        {
            List<Room> rooms = new();
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
            return rooms;
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
            using (writer = new(ItemsFilePath, true))
            {
                writer.WriteLine(item.Name + "," + item.Description + "," + item.DetailedDescription);
            }
        }
        public static void AddRoomToFile(Room room)
        {
            string roomItemIds = string.Empty;
            room.ItemsById.ForEach(i => roomItemIds += i.ToString() + "§");
            roomItemIds = roomItemIds.Remove(roomItemIds.Length - 1);
            using (writer = new(RoomsFilePath, true))
            {
                writer.Write("\n" + room.Name + "," + room.Description + "," + room.DetailedDescription + "," + roomItemIds);
            }
        }
    }
}
