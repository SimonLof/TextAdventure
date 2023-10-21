using TextAdventure.Classes.EffectClasses;

namespace TextAdventure.Classes
{
    public static class FileHandler
    {
        private static readonly string RoomsFilePath = ".\\rooms.txt";
        private static readonly string ItemsFilePath = ".\\items.txt";
        private static readonly string InteractionFilePath = ".\\interactions.txt";
        private static readonly string ErrorLogFilePath = ".\\errorlog.txt";
        //private static readonly string? MapFilePath = ".\\map.txt"; // Save current map to continue playing? Later project
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
                                string[] effectsFromFile = roomProperties[4].Split("§");
                                room.EffectOnEnter = GetEffects(effectsFromFile);
                            }
                            rooms.Add(room);
                        }
                    }
                }
                else
                { // Base room file creator.
                    RoomFileInitializer();
                    rooms = GetRooms();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, $"Some rooms may be corrupted. Check {RoomsFilePath} formatting errors. Delete the file if problem persists.");
            }
            return rooms;
        }

        public static List<Item> GetItems()
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
                            string[] itemProperties = reader.ReadLine().Split(',');
                            Item item = new(itemProperties[0], itemProperties[1], itemProperties[2]);
                            if (itemProperties.Length > 3)
                            {
                                string[] effectsFromFile = itemProperties[3].Split('§');
                                item.ItemEffects = GetEffects(effectsFromFile);
                            }
                            items.Add(item);
                        }
                    }
                }
                else
                { // Base item file creation
                    ItemFileInitializer();
                    items = GetItems();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, $"Some items may be corrupted. Check {ItemsFilePath} for formatting errors. Delete the file if problem persists.");
            }
            return items;
        }

        public static List<ItemInteraction> GetInteractions()
        {  // Reader done. Just make a file formatted correctly.
            List<ItemInteraction> interactions = new();
            try
            {
                if (File.Exists(InteractionFilePath))
                {
                    using (reader = new(InteractionFilePath))
                    {
                        while (!reader.EndOfStream)
                        {
                            string[] interactionProperties = reader.ReadLine().Split(',');
                            ItemInteraction interaction = new(int.Parse(interactionProperties[1]), int.Parse(interactionProperties[2]));
                            if (interactionProperties.Length > 3)
                            {
                                string[] effectsFromFile = interactionProperties[3].Split('§');
                                interaction.CombineEffects = GetEffects(effectsFromFile);
                            }
                            interactions.Add(interaction);
                        }
                    }
                }
                else
                {
                    InteractionFileInitializer();
                    interactions = GetInteractions();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, $"Some item combinations may be corrupted. Check {InteractionFilePath} for formatting errors. Delete the file if problem persists.");
            }
            return interactions;
        }
        public static void AddItemToFile(Item item)
        { // StringFromEffects not yet fully implemented. Add items to file via string method for now.
            try
            {
                string? firstItem = "";
                if (File.Exists(ItemsFilePath)) firstItem = "\n";
                using (writer = new(ItemsFilePath, true))
                {
                    string? writeItem = string.Empty;
                    writeItem = firstItem + item.Name + "," + item.Description + "," + item.DetailedDescription;
                    if (item.ItemEffects.Count > 0)
                    {
                        writeItem += StringFromEffects(item.ItemEffects);
                    }
                    writer.Write(writeItem);
                }
            }
            catch (Exception ex) { LogError(ex); }
        }

        private static string StringFromEffects(List<Effect> effects)
        { // Go through all effects and add correct string
            string? effectString = ",";
            foreach (Effect effect in effects)
            {
                effectString += effect.Name;
                switch (effect.Name)
                { // Todo: all this
                    case "show_text":
                        effectString += "$" + ((ShowTextEffect)effect).Text + "$" + ((ShowTextEffect)effect).TextDelay;
                        break;
                    case "unlock":
                        break;
                    case "win":
                        break;
                    case "lose":
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
            return effectString[..^1]; // remove the last §
        }
        public static void AddInteractionToFile(ItemInteraction interaction)
        {
            // not yet implemented.
        }

        public static void AddInteractionToFile(string interaction)
        { // Add interaction to file via exact string.
            try
            {
                string? firstInteraction = "";
                if (File.Exists(ItemsFilePath)) firstInteraction = "\n";
                using (writer = new(ItemsFilePath, true))
                {
                    writer.Write(firstInteraction + interaction);
                }
            }
            catch (Exception ex) { LogError(ex); }
        }

        public static void AddItemToFile(string item)
        { // Add item to file via exact string.
            try
            {
                string? firstItem = "";
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
                string? roomItemIds = string.Empty;
                bool firstRoom = true;
                room.ItemsById.ForEach(i => roomItemIds += i.ToString() + "§");
                roomItemIds = roomItemIds.Remove(roomItemIds.Length - 1);
                if (File.Exists(RoomsFilePath)) firstRoom = false;
                using (writer = new(RoomsFilePath, true))
                {
                    string writeRoom = string.Empty;
                    writeRoom = ((firstRoom ? "" : "\n") + room.Name + "," + room.Description + "," + room.DetailedDescription + "," + roomItemIds);
                    if (room.EffectOnEnter.Count > 0)
                    { // string from effect not complete. Use exact string to add stuff.
                        writeRoom += StringFromEffects(room.EffectOnEnter);
                    }
                    writer.Write(writeRoom);
                }
            }
            catch (Exception ex) { LogError(ex); }
        }
        public static void AddRoomToFile(string room)
        { // Add room to file via exact string.
            try
            {
                string? firstRoom = "";
                if (File.Exists(RoomsFilePath)) firstRoom = "\n";
                using (writer = new(RoomsFilePath, true))
                {
                    writer.Write(firstRoom + room);
                }
            }
            catch (Exception ex) { LogError(ex); }
        }
        #region Error Logging
        public static void LogError(Exception ex)
        {
            try
            {
                using (writer = new(ErrorLogFilePath, true))
                {
                    writer.WriteLine(DateTime.Now + " " + ex.ToString());
                }
            }
            catch
            {
                Console.WriteLine(DateTime.Now + " Something went wrong when writing to errorlog. Could not write error: " + ex.ToString());
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
                Console.WriteLine(DateTime.Now + " Something went wrong when writing to errorlog. Could not write error: " + optionalMessage + "\n" + ex.ToString());
            }
        }
        #endregion

        private static List<Effect> GetEffects(string[] effectsFromFile)
        {
            List<Effect> effects = new();
            for (int i = 0; i < effectsFromFile.Length; i++)
            {
                string[] effectNameAndVariable = effectsFromFile[i].Split("$");
                switch (effectNameAndVariable[0])
                {
                    case "show_text":
                        if (effectNameAndVariable.Length > 2)
                        {
                            for (int j = 0; j < Item.GetAllItems().Count; j++)
                                effectNameAndVariable[1] = effectNameAndVariable[1].Replace("{" + j + "}", Item.GetItemFromId(j).Name);
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
                    case "lose":
                        effects.Add(new LoseTheGameEffect());
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
            return effects;
        }

        #region File Initializers
        public static void ItemFileInitializer()
        {
            using (writer = new(ItemsFilePath, false))
            {
                writer.Write(
                    "Pouch,All kinds of crap inside,Maybe use it to get what's inside?,add_item_inv$1§remove_item_inv$0§show_text$You find a key in the bag! Nothing else in there so you throw the bag away.\r\n" +
                    "Key,Old rusty key,You think it might open the exit door...,unlock\r\n" +
                    "Book,Has a magical aura about it....,The book almost feels alive. You don't know what'll happen if you use it.,show_text$The book starts flying around and a ghost wizard shows up! He starts talking and asks... §ask_riddle${P}! What is your favorite color? $blue@green@white@black@yellow@purple@pink@brown@gray@grey@red@orange$Correct! The wizard fades away while saying... {P}... You are surprisingly self aware...$1$0\r\n" +
                    "Hat,Very fancy,It looks like a purple cone hat from the future. Not useful whatsoever. I promise...,show_text$You feel very fancy wearing it. You decide to save it for later.\r\n" +
                    "Jorts,Jean shorts,You really like how they feel,win\r\n" +
                    "Club,Blunt and made of metal,Pretty rusty. Might break after using.,remove_item_inv$5§show_text$The handle breaks in half and the head of the club falls off and breaks when hitting the ground. It's now unusable. You throw away the scraps. \r\n" +
                    "Robe,And old purple robe,It's pretty fancy but also very old.,show_text$You will destroy it if you try to put it on... Maybe don't do it.\r\n" +
                    "Magazine,A riddle magazine,If you answer the riddle you might get a prize!,show_text$You open the magazine and flip the pages to the easiest riddle...§ask_riddle${P}! I fell in a puddle... knee first... where the astronauts called for help. Who am I? $whitney houston$A white powder poofs everywhere and everything fades to black.... You wake up some time later with powder in your pocket.$1$8\r\n" +
                    "Powder,A white powder,You are intrigued by what seems like a magical quality of the powder....,lose");
            }
        }

        public static void RoomFileInitializer()
        {
            using (writer = new(RoomsFilePath, false))
            {
                writer.Write(
                    "Tiny closet,A small closet. You don't know how you got here.,It's very crammed.,3\r\n" +
                    "Bedroom,A bedroom. It clearly hasn't been occupied for quite some time.,How do you get out of here? Maybe examine some items.,5§6\r\n" +
                    "Kitchen,A small kitchen. Empty pots and pans everywhere.,You see some cockroaches who seem to be building a nest... strange.,4\r\n" +
                    "Living room,Very empty. A couch and a fireplace is all there is...,Everything is covered in dust... You swipe some dust off of the fireplace and find a small note that says \"A wizard wears a hat and robe in combination\".,7\r\n" +
                    "The End,Ending Room,Can't see this. You finish when you enter.,999,show_text$This is the end.......$200§win");
            }
        }

        public static void InteractionFileInitializer()
        {
            using (writer = new(InteractionFilePath, false))
            {
                writer.Write("Magic Wizard,6,3,show_text$You combined {6} and {3} into a fancy wizard outfit that starts moving around on its own!$20§show_text$.....$250§show_text$The outfit bursts into flames and leaves behind a book on the floor.$20§remove_item_inv$6§remove_item_inv$3§add_item_room$2");
            }
        }
        #endregion
    }
}
