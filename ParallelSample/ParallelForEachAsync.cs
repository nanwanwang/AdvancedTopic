using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks.Dataflow;

namespace ParallelSample;

public class ParallelForEachAsync
{
    public async Task TestParallelForEachAsync()
    {
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

    }


    /// <summary>
    /// Get dir
    /// </summary>
    /// <param name="path"></param>
    /// <param name="searchPattern"></param>
    /// <param name="searchOption"></param>
    /// <returns></returns>
    public static long DirectoryFileSize(string path, string searchPattern, SearchOption searchOption)
    {
        var files = Directory.EnumerateFiles(path, searchPattern, searchOption);

        long masterTotal = 0;
        ParallelLoopResult result = Parallel.ForEach<string, long>(files, () => { return 0; }
        , (file, loopSate, index, taskLocalTotal) =>
        {
            long fileLength = 0;
            FileStream fileStream = null;
            try
            {
                fileStream = File.OpenRead(file);
                fileLength = fileStream.Length;
            }
            catch (Exception)
            {


            }
            finally
            {
                if (fileStream != null) fileStream.Dispose();
            }
            return taskLocalTotal + fileLength;
        },
        taskLocalTotal => {
            Interlocked.Add(ref masterTotal, taskLocalTotal);
        });

        return masterTotal;
    }

    public static int ParallelSum0(IEnumerable<int> vs)
    {
        object mutex = new object();
        int result = 0;

        Parallel.ForEach(vs, localInit: () => 0, body: (item, state, localValue) => localValue + item, localFinally: localValue =>
         {
             lock (mutex)
             {
                 result += localValue;
             }
         });

         return result;
       
        
    }


    public static int ParallelSum1(IEnumerable<int> values)
    {
         return values.AsParallel().Sum();
    }


    public static int ParallelSum2(IEnumerable<int> vs)
    {
        return vs.AsEnumerable().Aggregate(seed:0,func:(sum,item)=>sum+ item);
    }

}

public class GitHubUser
{
    public string Name { get; set; }
    public string Bio { get; set; }
}

