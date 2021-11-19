using System.Net.Http.Headers;
using System.Net.Http.Json;

var userHandlers = new[]
{
    "users/okyrylchuk",
    "users/shanselman",
    "users/jaredpar",
    "users/davidfowl"
};

using HttpClient client = new()
{
    BaseAddress = new Uri("https://api.github.com"),
};
client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("DotNet", "6"));

ParallelOptions parallelOptions = new()
{
    MaxDegreeOfParallelism = 3
};

await Parallel.ForEachAsync(userHandlers, parallelOptions, async (uri, token) =>
{
    var user = await client.GetFromJsonAsync<GitHubUser>(uri, token);

    Console.WriteLine($"Name: {user.Name}\nBio: {user.Bio}\n");
});

public class GitHubUser
{
    public string Name { get; set; }
    public string Bio { get; set; }
}