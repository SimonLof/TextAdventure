using TextAdventure.Classes;

internal class Program
{
    private static void Main(string[] args)
    {
        Item myItem = new("Key", "00", "A key for the exit.");

        Console.WriteLine(myItem.Name + ": " + myItem.Description);
    }
}