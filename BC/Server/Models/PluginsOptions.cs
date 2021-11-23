namespace BC.Server.Models;

public class PluginsOptions
{
    public List<PluginOption> PluginOptions { get; set; }
}

public class PluginOption
{
    public string Name { get; set; }
    public string Url { get; set; }
}