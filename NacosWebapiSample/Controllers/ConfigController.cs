using Microsoft.AspNetCore.Mvc;

namespace NacosWebapiSample.Controllers;

[ApiController]
[Route("[controller]")]
public class ConfigController:ControllerBase
{
    private readonly ILogger<ConfigController> _logger;
    private readonly IConfiguration _configuration;

    public ConfigController(ILogger<ConfigController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet("getconfig")]
    public UserInfo GetConfig()
    {
        
        var userInfo1 = _configuration.GetSection("UserInfo").Get<UserInfo>();

        var commomValue = _configuration["commonkey"];
        var demoValue = _configuration["demokey"];
        _logger.LogInformation("commonkey:" + commomValue);
        _logger.LogInformation("demokey:" + demoValue);

        return userInfo1;
    }
}