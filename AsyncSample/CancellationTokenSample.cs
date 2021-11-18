
namespace AsyncSample;
public class CancellationTokenSample
{
    /// <summary>
    /// 获取天气信息
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns></returns>
    public static async Task GetWeatherAsync(CancellationToken cancellationToken)
    {
        HttpClient httpClient = new HttpClient();
        var res = await httpClient.GetAsync("http://www.weather.com.cn/data/sk/101110101.html", cancellationToken);
        var result = await res.Content.ReadAsStringAsync();
        Console.WriteLine(result);
    }

    public static async Task GetWeatherAsync2(CancellationToken token1, CancellationToken token2, CancellationToken token3)
    {
        var res = await new HttpClient().GetAsync("http://www.weather.com.cn/data/sk/101110101.html", token1);
        var result = await res.Content.ReadAsStringAsync();
        Console.WriteLine("cts1:{0}", result);

        var res2 = await new HttpClient().GetAsync("http://www.weather.com.cn/data/sk/101110101.html", token2);
        var result2 = await res2.Content.ReadAsStringAsync();
        Console.WriteLine("cts2:{0}", result2);

        var res3 = await new HttpClient().GetAsync("http://www.weather.com.cn/data/sk/101110101.html", token3);
        var result3 = await res2.Content.ReadAsStringAsync();
        Console.WriteLine("cts3:{0}", result3);
    }

    public static async Task LongRunningAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            Console.WriteLine("一直运行");
            await Task.Delay(1000);
        }

        //while (true)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();
        //}
    }


    public static async Task<string> OperationAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);
        return "ok";
    }
}

