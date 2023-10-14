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
                            string[] roomProperties = reader.ReadLine().Split(",");
                            string[] itemIds = roomProperties[3].Split("§");
                            Room room = new(roomProperties[0], roomProperties[1], roomProperties[2], itemIds.Select(i => int.Parse(i)).ToList());
                            if (roomProperties.Length > 4)
                            {
                                List<Effect> effects = new List<Effect>();
                                string[] effectsFromFile = roomProperties[4].Split("§");
                                for (int i = 0; i < effectsFromFile.Length; i++)
                                {
                                    string[] effectNameAndVariable = effectsFromFile[i].Split("$");
                                    switch (effectNameAndVariable[0])
                                    {
                                        case "show_text":
                                            if (effectNameAndVariable.Length > 2)
                                            {
                                                effects.Add(new ShowTextEffect(effectNameAndVariable[1], int.Parse(effectNameAndVariable[2])));
                                            }
                                            else { effects.Add(new ShowTextEffect(effectNameAndVariable[1])); }
                                            break;
                                        case "unlock":
                                            effects.Add(new UnlockEffect());
                                            break;
                                        case "win":
                                            effects.Add(new WinTheGameEffect());
                                            break;
                                        case "add_item_inv":
                                            effects.Add(new AddItemToInventoryEffect(int.Parse(effectNameAndVariable[1])));
                                            break;
                                        case "add_item_room":
                                            effects.Add(new AddItemToRoomEffect(int.Parse(effectNameAndVariable[1])));
                                            break;
                                        case "remove_item_inv":
                                            effects.Add(new RemoveItemFromInventoryEffect(int.Parse(effectNameAndVariable[1])));
                                            break;
                                        case "remove_item_room":
                                            effects.Add(new RemoveItemFromRoomEffect(int.Parse(effectNameAndVariable[1])));
                                            break;
                                        case "ask_riddle":
                                            string[] riddleAnswers = effectNameAndVariable[2].Split('@');
                                            effects.Add(new AskARiddleEffect(effectNameAndVariable[1],
                                                                                      riddleAnswers,
                                                                                      effectNameAndVariable[3],
                                                                                      int.Parse(effectNameAndVariable[4]) > 0,
                                                                                      int.Parse(effectNameAndVariable[5])));
                                            break;
                                    }

                                }
                                room.EffectOnEnter = effects;
                            }
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
            catch (Exception ex)
            {
                LogError(ex);
                return rooms;
            }
        }

        public static List<Item> GetAllItems()
        {
            List<Item> items = new();
            try
            {
                if (File.Exists(ItemsFilePath))
                {
                    using (reader = new(ItemsFilePath))
                    {
                        while (!reader.EndOfStream)
                        {
                            string[] line = reader.ReadLine().Split(',');
                            Item item = new(line[0], line[1], line[2]);
                            if (line.Length > 3)
                            {
                                try
                                {
                                    string[] effects = line[3].Split("§");
                                    for (int i = 0; i < effects.Length; i++)
                                    {
                                        string[] effectNameAndVariable = effects[i].Split("$");
                                        switch (effectNameAndVariable[0])
                                        {
                                            case "show_text":
                                                if (effectNameAndVariable.Length > 2)
                                                {
                                                    item.ItemEffects.Add(new ShowTextEffect(effectNameAndVariable[1], int.Parse(effectNameAndVariable[2])));
                                                }
                                                else { item.ItemEffects.Add(new ShowTextEffect(effectNameAndVariable[1])); }
                                                break;
                                            case "unlock":
                                                item.ItemEffects.Add(new UnlockEffect());
                                                break;
                                            case "win":
                                                item.ItemEffects.Add(new WinTheGameEffect());
                                                break;
                                            case "add_item_inv":
                                                item.ItemEffects.Add(new AddItemToInventoryEffect(int.Parse(effectNameAndVariable[1])));
                                                break;
                                            case "add_item_room":
                                                item.ItemEffects.Add(new AddItemToRoomEffect(int.Parse(effectNameAndVariable[1])));
                                                break;
                                            case "remove_item_inv":
                                                item.ItemEffects.Add(new RemoveItemFromInventoryEffect(int.Parse(effectNameAndVariable[1])));
                                                break;
                                            case "remove_item_room":
                                                item.ItemEffects.Add(new RemoveItemFromRoomEffect(int.Parse(effectNameAndVariable[1])));
                                                break;
                                            case "ask_riddle":
                                                string[] riddleAnswers = effectNameAndVariable[2].Split('@');
                                                item.ItemEffects.Add(new AskARiddleEffect(effectNameAndVariable[1],
                                                                                          riddleAnswers,
                                                                                          effectNameAndVariable[3],
                                                                                          int.Parse(effectNameAndVariable[4]) > 0,
                                                                                          int.Parse(effectNameAndVariable[5])));
                                                break;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogError(ex);
                                }
                            }
                            items.Add(item);
                        }
                    }
                }
                else
                { // Base items.
                    Item keyItem = new("Key", "Small and rusty.", "A small inscription on backside of the key says 'Key for the exit'.");
                    keyItem.ItemEffects.Add(new UnlockEffect());
                    items.Add(keyItem);
                    Item swordItem = new("Sword", "Old and rusty.", "You think it might break if you try to use it...");
                    swordItem.ItemEffects.Add(new RemoveItemFromInventoryEffect(swordItem.Id));
                    swordItem.ItemEffects.Add(new ShowTextEffect("... The sword shattered into a 1000 pieces. You cut your finger."));
                    items.Add(swordItem);
                    FileHandler.AddItemToFile(keyItem);
                    FileHandler.AddItemToFile(swordItem);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Some items may be corrupted. Check items.txt for formatting errors.");
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
                    string writeItem = string.Empty;
                    writeItem = firstItem + item.Name + "," + item.Description + "," + item.DetailedDescription;
                    if (item.ItemEffects.Count > 0)
                    {
                        string effectString = ",";
                        foreach (var effect in item.ItemEffects)
                        {
                            effectString += effect.Name;
                            switch (effect.Name)
                            {
                                case "show_text":
                                    effectString += "$" + (effect as ShowTextEffect).Text;
                                    break;
                                case "unlock":
                                    break;
                                case "add_item_inv":
                                    break;
                                case "add_item_room":
                                    break;
                                case "remove_item_inv":
                                    break;
                                case "remove_item_room":
                                    break;
                                case "ask_riddle":
                                    break;
                            }
                            effectString += "§";
                        }
                        writeItem += effectString.Substring(0, effectString.Length - 1);
                    }
                    writer.Write(writeItem);
                }
            }
            catch (Exception ex) { LogError(ex); }
        }
        public static void AddItemToFile(string item)
        {
            try
            {
                string firstItem = "";
                if (File.Exists(ItemsFilePath)) firstItem = "\n";
                using (writer = new(ItemsFilePath, true))
                {
                    writer.Write(firstItem + item);
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
        public static void LogError(Exception ex, string optionalMessage)
        {
            try
            {
                using (writer = new(ErrorLogFilePath, true))
                {
                    writer.WriteLine(DateTime.Now + " " + optionalMessage + "\n" + ex.ToString());
                }
            }
            catch
            {
                Console.WriteLine(DateTime.Now + " " + optionalMessage + "\n" + ex.ToString());
            }
        }
    }
}
