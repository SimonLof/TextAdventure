namespace TextAdventure.Classes
{
    public static class CreationMode
    {
        public static void CreateStuff()
        { // put this in its own class?
            Map map = new Map();
            string? userInput = "";
            while (userInput != "q")
            {
                FileHandler.GetAllItems();
                Console.WriteLine("Add (r)oom or add (i)tem. Map will be generated from the rooms. Get (a)ll items and their index, for room construction. (q)uit.");
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "r":
                        AddRoom();
                        break;
                    case "i":
                        AddItem();
                        break;
                    case "a":
                        try
                        {
                            ListItems();
                            break;
                        }
                        catch { break; }
                }
                Item.ResetItemList();
            }
            ScreenWriter.ConsoleWrite("Quitting creator mode");
            ScreenWriter.ConsoleWriteLine(".......", 250);
            Console.WriteLine("Restart the app to make sure added things get loaded correctly.");
        }

        private static void AddItem()
        {
            Console.WriteLine("\"<Name>,<Description>,<Detailed Description>,<effect_1_name>$<effect_1_variable>§" +
                "<effect_2_name>$<effect_2_variable>$<effect_2_variable_list_var_1>@<effect2_variable_list_var_2>\" \n" +
                "Id is automatic(check id after creation), item-interaction are its own thing. Notice difference between §, $ and @. " +
                "'e' for list of effects and explanations."); // Must follow this exactly or item will break...
            string? makeItem = Console.ReadLine();
            if (!(makeItem is "" or null))
            {
                if (makeItem.ToLower().Trim() == "e")
                {
                    Console.WriteLine("show_text$<text to show> - Show a string of text.\n" +
                                      "add_item_inv$<id of item to add> - Add item to player inventory.\n" +
                                      "add_item_room$<id of item to add> - Add item to room.\n" +
                                      "lightning(optional:)$<amount of repetitions>$<delay in ms between flashes>" +
                                      "(not yet implemented)$color - Lightning effect.Time of flash is delay / 2.Might change later.\n" +
                                      "remove_item_inv$<id of item to remove> - Remove item from player inventory.\n" +
                                      "remove_item_room$<id of item to remove> - Remove item from room.\n" +
                                      "beep(optional:)$<frequency of beep>$<duration of beep in ms> - Play a beep with " +
                                      "selected frequency and duration.Default is 200hz, 200ms. Not yet functional.\n" +
                                      "unlock - Unlocks any locked door in the room. Only the last door is locked. Might change in the future.\n" +
                                      "win - Plays the ending sequence and exits the application in the end.\n");
                }
                else
                {
                    try
                    { // Just prints the exact string to the file.
                        FileHandler.AddItemToFile(makeItem);
                        Console.WriteLine(makeItem.Split(',')[0] + " added.");
                        #region Not Yet Implemented
                        // *****When I make the item creation process more user friendly all this code is a starting point to make item from text.
                        //List<string> itemProps = makeItem.Split(',').ToList();
                        //Item item = new(itemProps[0], itemProps[1], itemProps[2]);
                        //if (itemProps.Count > 3)
                        //{
                        //    string[] effects = itemProps[3].Split("§");
                        //    for (int i = 0; i < effects.Length; i++)
                        //    {
                        //        string[] effectNameAndVariable = effects[i].Split("$");
                        //        switch (effectNameAndVariable[0])
                        //        {
                        //            case "show_text":
                        //                item.ItemEffects.Add(new ShowTextEffect(effectNameAndVariable[1]));
                        //                break;
                        //            case "unlock":
                        //                item.ItemEffects.Add(new UnlockEffect());
                        //                break;
                        //            case "add_item_inv":
                        //                item.ItemEffects.Add(new AddItemToInventoryEffect(int.Parse(effectNameAndVariable[1])));
                        //                break;
                        //            case "add_item_room":
                        //                item.ItemEffects.Add(new AddItemToRoomEffect(int.Parse(effectNameAndVariable[1])));
                        //                break;
                        //            case "remove_item_inv":
                        //                item.ItemEffects.Add(new RemoveItemFromInventoryEffect(int.Parse(effectNameAndVariable[1])));
                        //                break;
                        //            case "remove_item_room":
                        //                item.ItemEffects.Add(new RemoveItemFromRoomEffect(int.Parse(effectNameAndVariable[1])));
                        //                break;
                        //            case "ask_riddle":
                        //                string[] riddleAnswers = effectNameAndVariable[3].Split('@');
                        //                item.ItemEffects.Add(new AskARiddleEffect(int.Parse(effectNameAndVariable[1]), effectNameAndVariable[2], riddleAnswers));
                        //                break;
                        //        }
                        //    }
                        //}
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private static void AddRoom()
        { // Effects not yet implemented in room maker.
            Console.WriteLine("\"<Name>,<Description>,<Detailed Description>,<item1>§<item2>§<item3>,<effect_1_name>$<effect_1_var>§<effect_2_name>\"" +
                "\nIf no items just type 999.");
            string? makeRoom = Console.ReadLine();
            if (!(makeRoom is "" or null))
            {
                try
                {
                    List<string> roomProps = makeRoom.Split(',').ToList();
                    List<string> roomItemIds = roomProps[3].Split('§').ToList();
                    Room room = new(roomProps[0], roomProps[1], roomProps[2], roomItemIds.Select(i => int.Parse(i)).ToList());
                    FileHandler.AddRoomToFile(room);
                    Console.WriteLine(room.Name + " added.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void ListItems()
        {
            foreach (Item item in Item.GetAllItems())
            {
                ScreenWriter.ConsoleWriteLine(item.Id + " : " + item.Name + " : " + item.Description, 0);
            }
        }
    }
}