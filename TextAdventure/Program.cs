using TextAdventure.Classes;

internal class Program
{
    private static void Main(string[] args)
    {
        Item myItem = new("Key", "A key for the exit.");

        Console.WriteLine(myItem.Name + ": " + myItem.Description);
    }
}