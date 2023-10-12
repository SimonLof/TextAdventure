using TextAdventure.Classes.EffectClasses;

namespace TextAdventure.Classes
{
    public static class FileHandler
    {
        private static readonly string RoomsFilePath = ".\\rooms.txt";
        private static readonly string ItemsFilePath = ".\\items.txt";
        private static readonly string ErrorLogFilePath = ".\\errorlog.txt";
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
                { // base rooms
                    AddRoomToFile(new Room("Staring room", "A small closet. You don't know how you got here.", "It's very crammed.", new() { 0, 1 }));
                    AddRoomToFile(new Room("Kitchen", "A small kitchen. Empty pots and pans everywhere.", "You see some cockroaches who seem to be building a nest... strange.", new() { 2, 3 }));
                    rooms = GetRooms();
                }
                return rooms;
            }
            catch { return rooms; }
        }

        public static List<Item> GetAllItems(Map map)
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
                        try
                        {
                            string[] effects = line[3].Split("§");
                            for (int i = 0; i < effects.Length; i++)
                            {
                                string[] nameAndVariable = effects[i].Split("$");
                                switch (nameAndVariable[0])
                                {
                                    case "show_text":
                                        item.ItemEffects.Add(new ShowTextEffect(nameAndVariable[1]));
                                        break;
                                    case "unlock":
                                        item.ItemEffects.Add(new UnlockEffect(map));
                                        break;
                                    case "add_item_inv":
                                        item.ItemEffects.Add(new AddItemToInventoryEffect(int.Parse(nameAndVariable[1])));
                                        break;
                                    case "add_item_room":
                                        item.ItemEffects.Add(new AddItemToRoomEffect(int.Parse(nameAndVariable[1])));
                                        break;
                                    case "remove_item_inv":
                                        item.ItemEffects.Add(new RemoveItemFromInventoryEffect(int.Parse(nameAndVariable[1])));
                                        break;
                                    case "remove_item_room":
                                        item.ItemEffects.Add(new RemoveItemFromRoomEffect(int.Parse(nameAndVariable[1])));
                                        break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogError(ex);
                        }
                        items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            return items;
        }
        public static void AddItemToFile(Item item)
        {
            try
            {
                string firstItem = "";
                if (File.Exists(ItemsFilePath)) firstItem = "\n";
                using (writer = new(ItemsFilePath, true))
                {
                    writer.Write(firstItem + item.Name + "," + item.Description + "," + item.DetailedDescription);
                }
            }
            catch (Exception ex) { LogError(ex); }
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
            catch (Exception ex) { LogError(ex); }
        }

        public static void LogError(Exception ex)
        {
            try
            {
                using (writer = new(ErrorLogFilePath, true))
                {
                    writer.WriteLine(ex.ToString());
                }
            }
            catch
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
