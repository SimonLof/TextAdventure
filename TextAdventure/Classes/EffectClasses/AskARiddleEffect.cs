﻿namespace TextAdventure.Classes.EffectClasses
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
                    ScreenWriter.ConsoleWrite("Correct!");
                    if (ItemRewardId != 999)
                    {
                        ScreenWriter.ConsoleWriteLine(" You are rewarded with " + Item.AllItems.SingleOrDefault(i => i.Id == ItemRewardId).Name + "!");
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
    }
}
