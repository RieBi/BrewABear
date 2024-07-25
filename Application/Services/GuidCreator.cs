namespace Application.Services;
public class GuidCreator : IGuidCreator
{
    public string Create() => Guid.NewGuid().ToString();
}
