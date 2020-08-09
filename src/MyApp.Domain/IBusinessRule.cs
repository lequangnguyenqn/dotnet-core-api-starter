namespace MyApp.Domain
{
    public interface IBusinessRule
    {
        bool IsBroken();

        string Message { get; }
    }
}
