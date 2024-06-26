namespace InGreedIoApi.Model;

public class AuthResult
{
    public string Token { get; set; }
    public bool Result { get; set; }
    public IList<string> Roles { get; set; }
    public List<string> Errors { get; set; }
}
