namespace Emando.Vantage.Workflows.Competitions
{
    public interface IDistanceDisciplineExpertManager
    {
        string[] GetKeys();

        IDistanceDisciplineExpert Find(string discipline);
    }
}