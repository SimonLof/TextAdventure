using TextAdventure.Classes.EffectClasses;

namespace TextAdventure.Classes
{
    public static class InputHandler
    {
        #region Game input handle
        public static void GetOutcome(string inputString, ref Player player, ref Map map, ref bool running)
        {
            try
            {
                while (inputString.Contains("  "))
                {
                    inputString = inputString.Trim().Replace("  ", " ");
                }
                List<string> inputCommands = inputString.Split(' ').ToList();
                int numberOfCommands = inputCommands.Count;
                switch (inputCommands[0].ToLower())
                { // alla kommandon borde vara objekt...
                    case "q" or "quit" or "exit":
                        running = Quit(running);
                        break;
                    case "help" or "h":
                        ScreenWriter.ConsoleWriteLine("'Look' at the room your are in. 'Go <direction>' to go somewhere. 'get <item>' to pick up stuff.\n" +
                                                      "'inv' to look at your inventory. 'inspect <most things>' to take a closer look at something.\n" +
                                                      "'use <item>' to use an item. 'use <item> on <item>' to combine stuff. 'q' to quit.", 0);
                        break;
                    case "use" or "u":
                        UseItem(player, inputCommands, numberOfCommands);
                        break;
                    case "go" or "walk" or "run" or "g":
                        GoInDirection(map, inputCommands, numberOfCommands);
                        break;
                    case "get" or "take" or "t":
                        GetItem(player, map, inputCommands);
                        break;
                    case "drop" or "d":
                        DropItem(player, map, inputCommands);
                        break;
                    case "inv" or "inventory" or "bag":
                        CheckInventory(player);
                        break;
                    case "inspect" or "examine" or "e":
                        InspectSomething(player, map, inputCommands);
                        break;
                    case "look" or "l":
                        LookAround(map, numberOfCommands);
                        break;
                    default:
                        throw new Exception("I don't know what you mean by that... See list of commands with 'help'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.Message != "I don't know what you mean by that... See list of commands with 'help'.") { FileHandler.LogError(ex); }
            }
        }

        private static void DropItem(Player player, Map map, List<string> inputCommands)
        {
            List<Effect> effects = new List<Effect>();
            if (inputCommands.Count > 1)
            {
                Item item = player.Inventory.FirstOrDefault(i => i.Name.ToLower() == inputCommands[1].ToLower());
                if (item != null)
                {
                    effects.Add(new RemoveItemFromInventoryEffect(item.Id));
                    effects.Add(new AddItemToRoomEffect(item.Id));
                    effects.Add(new ShowTextEffect($"You dropped {item.Name} on the floor of the {map.CurrentRoom.Name}."));
                }
                else { effects.Add(new ShowTextEffect($"Nothing named {inputCommands[1]} in inventory.")); }
            }
            else { effects.Add(new ShowTextEffect("Drop what?")); }
            foreach (var effect in effects)
                effect.DoEffect();
        }

        private static bool Quit(bool running)
        {
            ScreenWriter.ConsoleWrite("Are you sure?(y/n) ", 0);
            if (Console.ReadLine().ToLower().Trim() == "y")
            {
                running = false;
            }
            else
                ScreenWriter.ConsoleWriteLine("Aborted quit.", 0);
            return running;
        }

        private static void UseItem(Player player, List<string> inputCommands, int numberOfCommands)
        {
            if (numberOfCommands > 1)
            {
                bool itemFound = false;
                foreach (Item item in player.Inventory)
                {
                    if (item.Name.ToLower() == inputCommands[1])
                    {
                        itemFound = true;
                        if (inputCommands.Count > 2)
                        {
                            try
                            {
                                if (inputCommands[2].ToLower() == "on")
                                {
                                    if (inputCommands.Count > 3)
                                    {
                                        Item firstItem = player.Inventory.SingleOrDefault(i => i.Name.ToLower() == inputCommands[1].ToLower());
                                        Item secondItem = player.Inventory.SingleOrDefault(i => i.Name.ToLower() == inputCommands[3].ToLower());
                                        if (secondItem != null && firstItem != null)
                                        {
                                            if (ItemInteraction.InteractionExists(firstItem, secondItem))
                                            {
                                                ItemInteraction interaction = ItemInteraction.AllInteractions.SingleOrDefault(i =>
                                                                                      (i.FirstItemId == firstItem.Id && i.SecondItemId == secondItem.Id) ||
                                                                                      (i.FirstItemId == secondItem.Id && i.SecondItemId == firstItem.Id));
                                                if (interaction.CombineEffects.Count > 0)
                                                {
                                                    interaction.CombineEffect();
                                                }
                                                else { ScreenWriter.ConsoleWriteLine("No interaction effects in interaction effect list. Complain to the programmer."); }
                                                break;
                                            }
                                            else
                                                ScreenWriter.ConsoleWriteLine($"Can't use {inputCommands[1]} with {inputCommands[3]}.");
                                        }
                                        else if (firstItem != null && inputCommands[3].ToLower() is "door" or "exit")
                                        {
                                            if (firstItem.Name.ToLower() == "key")
                                            {
                                                firstItem.ItemEffects.SingleOrDefault(e => e.Name == "unlock").DoEffect();
                                            }
                                            else
                                            {
                                                ScreenWriter.ConsoleWriteLine($"Can't use {inputCommands[1]} on {inputCommands[3]}.");
                                            }
                                        }
                                        else
                                        {
                                            ScreenWriter.ConsoleWriteLine($"{inputCommands[3]} not found in inventory.");
                                        }
                                    }
                                }
                                else
                                    ScreenWriter.ConsoleWriteLine("I don't understand... Use 'help' for valid commands.");
                            }
                            catch (Exception ex)
                            {
                                FileHandler.LogError(ex);
                            }
                        }
                        else
                        {
                            try
                            {
                                if (item.ItemEffects.Count > 0)
                                {
                                    foreach (Effect effect in item.ItemEffects)
                                    {
                                        effect.DoEffect();
                                        if (effect is AskARiddleEffect)
                                        {
                                            if (((AskARiddleEffect)effect).Correct && ((AskARiddleEffect)effect).DestroyItemAfterCorrect)
                                            {
                                                RemoveItemFromInventoryEffect removeItem = new(item.Id);
                                                removeItem.DoEffect();
                                            }
                                        }
                                    }
                                }
                                else
                                    ScreenWriter.ConsoleWriteLine($"Can't use {inputCommands[1]}.");
                                break;
                            }
                            catch (Exception ex)
                            {
                                FileHandler.LogError(ex);
                            }
                        }
                    }
                }
                if (!itemFound) { ScreenWriter.ConsoleWriteLine($"Couldn't find {inputCommands[1]}."); }
            }
            else
                ScreenWriter.ConsoleWriteLine("What do you want to use?");
        }

        private static void GetItem(Player player, Map map, List<string> inputCommands)
        {
            if (inputCommands.Count > 1)
            {
                List<Item> roomItems = map.CurrentRoom.GetItemsInRoom();
                if (roomItems.Count > 0)
                {
                    bool itemFound = false;
                    foreach (Item item in roomItems)
                    {
                        if (item.Name.ToLower() == inputCommands[1].ToLower())
                        {
                            player.PickUpItem(item);
                            map.CurrentRoom.ItemsById.Remove(item.Id);
                            ScreenWriter.ConsoleWriteLine($"You picked up {item.Name}.");
                            itemFound = true;
                            break;
                        }
                    }
                    if (!itemFound)
                        ScreenWriter.ConsoleWriteLine($"No item named {inputCommands[1]} in this room.");
                }
            }
            else
                ScreenWriter.ConsoleWriteLine("Get what?");
        }

        private static void InspectSomething(Player player, Map map, List<string> inputCommands)
        { // Probably some way to do this way better.
            if (inputCommands.Count > 1)
            {
                BaseObject? lookingAt = player.Inventory.FirstOrDefault(i => i.Name.ToLower().Equals(inputCommands[1].ToLower()));
                //?? map.CurrentRoom.GetItemsInRoom().FirstOrDefault(i => i.Name.ToLower().Equals(inputCommands[1].ToLower()));   <-- Remove ; from row above and add this back if wanting to be able to examine items not in inventory.
                if (lookingAt == null)
                {
                    if (inputCommands[1].ToLower() is "door" or "exit" && inputCommands.Count == 2) // Gör en koll att det är mer än 2 ord.
                    {
                        if (map.CurrentRoom.Doors.Count == 1)
                            lookingAt = map.CurrentRoom.Doors[0];
                        else
                            lookingAt = new Item("Which door?", "", "'North' 'South' 'East' 'West'");
                    }
                    else
                    {
                        if (inputCommands.Count > 2)
                        {
                            Facing? facing = null;
                            switch (inputCommands[2].ToLower())
                            {
                                case "north":
                                    facing = Facing.North;
                                    break;
                                case "south":
                                    facing = Facing.South;
                                    break;
                                case "east":
                                    facing = Facing.East;
                                    break;
                                case "west":
                                    facing = Facing.West;
                                    break;
                                default:
                                    lookingAt = new Item("Which door?", "", "'North' 'South' 'East' 'West'");
                                    break;
                            }
                            if (facing != null)
                            {
                                lookingAt = map.CurrentRoom.Doors.SingleOrDefault(d =>
                                d.Name.ToLower() == inputCommands[1].ToLower() &&
                                d.Direction == facing);
                                if (lookingAt == null) { lookingAt = new Item("No door in that direction.", "", "Use 'look' to see avaliable doors."); }
                            }
                        }
                    }
                }
                if (lookingAt == null)
                { lookingAt = "room" == inputCommands[1].ToLower() ? map.CurrentRoom : null; }
                if (lookingAt == null)
                {
                    lookingAt = (inputCommands[1].ToLower() is "yourself" or "myself" or "me" ||
                                 inputCommands[1].ToLower() == player.Name.ToLower()) ?
                                 player : null;
                }
                if (lookingAt == null)
                {
                    ScreenWriter.ConsoleWrite($"Nothing here named");
                    for (int i = 1; i < inputCommands.Count; i++)
                        ScreenWriter.ConsoleWrite($" {inputCommands[i]}");
                    ScreenWriter.ConsoleWriteLine(".");
                }
                else
                {
                    ScreenWriter.ConsoleWriteLine($"{lookingAt.Name[0..1].ToUpper()}{lookingAt.Name[1..]} - {lookingAt.DetailedDescription}");
                    if (lookingAt.Name.ToLower() == "door")
                    {
                        ScreenWriter.ConsoleWriteLine("The door is " + (((Door)lookingAt).Locked ? "locked" : "unlocked") + ".");
                    }
                }
            }
            else
            {
                ScreenWriter.ConsoleWriteLine("What do you want to examine?");
            }
        }

        private static void LookAround(Map map, int numberOfCommands)
        {
            if (numberOfCommands <= 1)
            {
                ScreenWriter.ConsoleWriteLine("You see...", 150);
                foreach (Door door in map.CurrentRoom.Doors)
                {
                    ScreenWriter.ConsoleWriteLine($"A door facing {door.Direction}.");
                }
                ScreenWriter.ConsoleWriteLine($"{map.CurrentRoom.Name}  - {map.CurrentRoom.Description}");
                List<Item> items = map.CurrentRoom.GetItemsInRoom();
                if (items.Count > 0)
                {
                    ScreenWriter.ConsoleWriteLine("You also see some items...");
                    foreach (Item item in items)
                    {
                        ScreenWriter.ConsoleWriteLine($"{item.Name} - {item.Description}");
                    }
                }
                else
                {
                    ScreenWriter.ConsoleWriteLine("No items in this room.");
                }
            }
            else
            {
                ScreenWriter.ConsoleWriteLine("Just 'look'.");
            }
        }

        private static void GoInDirection(Map map, List<string> inputCommands, int numberOfCommands)
        {
            if (numberOfCommands > 1)
            {
                switch (inputCommands[1].ToLower())
                {
                    case "north":
                        LookForTheDoor(map, Facing.North);
                        break;
                    case "east":
                        LookForTheDoor(map, Facing.East);
                        break;
                    case "south":
                        LookForTheDoor(map, Facing.South);
                        break;
                    case "west":
                        LookForTheDoor(map, Facing.West);
                        break;
                    default:
                        ScreenWriter.ConsoleWriteLine($"Can't go {inputCommands[1]}.");
                        break;
                }
            }
            else
                ScreenWriter.ConsoleWriteLine("Go where?");
        }

        private static void CheckInventory(Player player)
        {
            if (player.Inventory.Count > 0)
            {
                foreach (Item item in player.Inventory)
                    ScreenWriter.ConsoleWriteLine($"{item.Name} - {item.Description}.");
            }
            else
                ScreenWriter.ConsoleWriteLine("Your inventory is empty.");
        }
        private static void LookForTheDoor(Map map, Facing facing)
        {
            if (map.CurrentRoom.Doors.Any(d => d.Direction == facing))
            {
                if (!map.CurrentRoom.Doors.SingleOrDefault(d => d.Direction == facing).Locked)
                {
                    ScreenWriter.ConsoleWriteLine($"Going {facing.ToString().ToLower()}...", 100);
                    map.CurrentRoom = map.CurrentRoom.Doors.SingleOrDefault(d => d.Direction == facing).LeadsToo;
                    ScreenWriter.ConsoleWriteLine(map.CurrentRoom.Description);
                    if (map.CurrentRoom.EffectOnEnter.Count > 0)
                    {
                        foreach (Effect effect in map.CurrentRoom.EffectOnEnter)
                        {
                            effect.DoEffect();
                        }
                    }
                }
                else
                {
                    ScreenWriter.ConsoleWriteLine("The door is locked!");
                }
            }
            else
            {
                ScreenWriter.ConsoleWriteLine("No door that way.");
            }
        }
        #endregion
    }
}
