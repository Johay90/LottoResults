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