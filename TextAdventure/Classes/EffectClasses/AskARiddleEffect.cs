namespace TextAdventure.Classes.EffectClasses
{
    public class AskARiddleEffect : Effect
    {
        public int ItemRewardId { get; set; }
        public string Riddle { get; set; }
        public string RiddleRewardString { get; set; }
        public string[] Answers { get; set; }
        public bool Usable { get; set; } = true;
        public bool Correct { get; set; } = false;
        public bool DestroyItemAfterCorrect { get; set; } = true;

        public AskARiddleEffect(string riddle, string[] answers, string rewardString, bool destroyItemAfterCorrect, int itemRewardId = 999)
        {
            Name = "ask_riddle";
            Riddle = riddle;
            Answers = answers;
            RiddleRewardString = rewardString;
            DestroyItemAfterCorrect = destroyItemAfterCorrect;
            ItemRewardId = itemRewardId;
        }

        public override void DoEffect()
        {
            try
            {
                Riddle = Riddle.Replace("{P}", Player.ThePlayer.Name); // Make a string manipulator class that can replace markers in files with variables.
                RiddleRewardString = RiddleRewardString.Replace("{P}", Player.ThePlayer.Name);
                ScreenWriter.ConsoleWrite(Riddle, 20);
                string userAnswer = Console.ReadLine();
                if (Usable)
                {
                    foreach (var answer in Answers)
                    {
                        if (answer.ToLower() == userAnswer.ToLower())
                        {
                            Correct = true;
                        }
                    }
                    if (Correct)
                    {
                        if (ItemRewardId != 999)
                        {
                            ScreenWriter.ConsoleWriteLine("...You are rewarded with " + Item.GetItemFromId(ItemRewardId).Name + "!");
                            AddItemToInventoryEffect addItemToInventoryEffect = new(ItemRewardId);
                            addItemToInventoryEffect.DoEffect();
                        }
                        ScreenWriter.ConsoleWriteLine(RiddleRewardString);
                    }
                    else
                    {
                        ScreenWriter.ConsoleWriteLine("Incorrect!");
                    }
                }
                else
                    ScreenWriter.ConsoleWriteLine("Can't use that.");
            }
            catch (Exception ex)
            {
                FileHandler.LogError(ex);
            }
        }

        public void DestroyAfterCorrect(Item item)
        {
            try
            {
                if (Correct && DestroyItemAfterCorrect)
                {
                    RemoveItemFromInventoryEffect removeThisItem = new(item.Id);
                    removeThisItem.DoEffect();
                }
            }
            catch (Exception ex)
            {
                FileHandler.LogError(ex);
            }
        }
    }
}
