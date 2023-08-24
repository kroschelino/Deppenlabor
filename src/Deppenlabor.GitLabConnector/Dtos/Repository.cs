namespace Deppenlabor.GitLabConnector.Dtos;

public class Repository
{
    public string? Name { get; set; }
    public string? Group { get; set; }
    public string? SshUrl { get; set; }
    public string? HttpUrl { get; set; }
}