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