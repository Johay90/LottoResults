var api = new ApiService();
var results = await api.GetAllResults();
Console.WriteLine("We have results from the following dates:");

for (int i = 0; i < results.Count; i++)
{
    Console.WriteLine($"{i + 1}. {results[i].DrawDate}");
}

do
{
    Console.WriteLine("Pick a lotto reuslt by typing it's ID.");
    string input = Console.ReadLine();
    if (int.TryParse(input, out int res))
    {
        res -= 1;
        if (results.ElementAtOrDefault(res) != null)
        {
            Console.WriteLine(results[res]);
            break;
        }
    }
} while (true);


Console.WriteLine("Application will now exit on keypress.");
Console.ReadKey();


public class LottoDraw
{
    public string DrawDate { get; set; }
    public List<int> Results { get; set; } = new List<int>();
    public int Jackpot { get; set; }
    public string Status { get; set; }

    public override string ToString()
    {
        string resultsString = string.Join(", ", Results);
        string formattedJackpot = Jackpot.ToString("N0"); 
        string rollover = Status.Length > 0 ? Status : "No Rollover";

        return $"{DrawDate} - Results: {resultsString} - Jackpot: £{formattedJackpot} - {rollover}";
    }
}