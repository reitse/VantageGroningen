using System.Collections.Generic;
using System.Reflection;

namespace Emando.Vantage.Workflows.Reporting
{
    public interface IDisciplineReportLoaderFactory
    {
        IEnumerable<string> GetKeys<T>(string discipline) where T : IReportLoader;

        T Create<T>(string discipline, string key) where T : IReportLoader;

        void Load(params Assembly[] assemblies);
    }
}