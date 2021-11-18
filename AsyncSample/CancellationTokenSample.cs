
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
}

