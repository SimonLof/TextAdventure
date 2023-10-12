namespace TextAdventure.Classes.EffectClasses
{
    public class AskARiddleEffect : Effect
    {
        public int ItemRewardId { get; set; }
        public string Riddle { get; set; }
        public string[] Answers { get; set; }

        public AskARiddleEffect(int itemRewardId, string riddle, string[] answers)
        {
            Name = "ask_riddle";
            ItemRewardId = itemRewardId;
            Riddle = riddle;
            Answers = answers;
        }

        public override void DoEffect()
        {
            ScreenWriter.ConsoleWrite(Riddle, 20);
            string userAnswer = Console.ReadLine();
            foreach (var answer in Answers)
            {
                if (answer.ToLower() == userAnswer.ToLower())
                {
                    ScreenWriter.ConsoleWriteLine("Correct! You are rewarded with " + Item.AllItems.SingleOrDefault(i => i.Id == ItemRewardId).Name + "!");
                    AddItemToInventoryEffect addItemToInventoryEffect = new(ItemRewardId);
                    addItemToInventoryEffect.DoEffect();
                }
            }
        }
    }
}
