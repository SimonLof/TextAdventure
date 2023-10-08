namespace TextAdventure.Classes
{
    public static class FileHandler
    {
        private static string RoomsFilePath = ".\\rooms.txt";
        private static string ItemsFilePath = ".\\items.txt";
        private static string MapFilePath = ".\\map.txt";
        private static StreamReader reader;
        private static StreamWriter writer;

        public static void WriteToFile(string text, string fileType)
        {
            switch (fileType.ToLower())
            {
                case "rooms":
                    break;
                case "items":
                    break;
                case "map":
                    break;
            }
        }

        public static List<Room> GetRooms()
        {
            List<Room> rooms = new();
            using (reader = new(RoomsFilePath))
            {
                while (!reader.EndOfStream)
                {
                    List<string> roomProps = reader.ReadLine().Split().ToList();
                    List<string> itemIds = roomProps[3].Split().ToList();
                    Room room = new(roomProps[0], roomProps[1], roomProps[2], itemIds.Select(i => int.Parse(i)).ToList());
                }
            }
            return rooms;
        }

        public static List<Item> GetItems()
        {
            List<Item> items = new();
            using (reader = new(ItemsFilePath))
            {
                while (!reader.EndOfStream)
                {
                    string[] line = reader.ReadLine().Split(',');
                    Item item = new(line[0], line[1], line[2]);
                    items.Add(item);
                }
            }
            return items;
        }
    }
}
