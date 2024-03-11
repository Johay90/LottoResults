using System.Text.RegularExpressions;
using HtmlAgilityPack;

public class ApiService
{
    public async Task<List<LottoDraw>> GetAllResults()
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync("https://www.lottery.co.uk/lotto/results/past");
        response.EnsureSuccessStatusCode();
        string htmlContent = await response.Content.ReadAsStringAsync();

        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(htmlContent);

        var table = htmlDoc.DocumentNode.SelectSingleNode("//table[@class='table lotto mobFormat']");
        var lottoDraws = new List<LottoDraw>();

        foreach (var row in table.SelectNodes(".//tr").Skip(1))
        {
            var cells = row.SelectNodes(".//td");
            if (cells != null && cells.Count >= 3)
            {
                var jackpotText = cells[2].SelectSingleNode(".//strong").InnerText.Trim();
                var cleanedJackpot = Regex.Replace(jackpotText, @"[^\d]", "");

                var draw = new LottoDraw
                {
                    DrawDate = cells[0].InnerText.Trim(),
                    Results = cells[1].SelectNodes(".//div[@class='result small lotto-ball' or @class='result small lotto-bonus-ball']")
                        .Select(node => int.Parse(node.InnerText.Trim()))
                        .ToList(),
                    Jackpot = int.Parse(cleanedJackpot),
                    Status = cells[2].SelectNodes(".//strong").Count() > 1 ? cells[2].SelectNodes(".//strong")[1].InnerText.Trim() : string.Empty
                };

                lottoDraws.Add(draw);
            }
        }
        return lottoDraws;
    }
}