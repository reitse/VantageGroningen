namespace Emando.Vantage
{
    public interface IUserSetting
    {
        string Key { get; }

        string Value { get; set; }
    }
}