using System;

namespace Emando.Vantage.Workflows.Reporting
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DisciplineReportAttribute : Attribute
    {
        private readonly string discipline;
        private readonly string key;

        public DisciplineReportAttribute(Type loaderType, string discipline, string key, int order = 0)
        {
            this.discipline = discipline;
            this.key = key;
            Order = order;
            LoaderType = loaderType;
        }

        public Type LoaderType { get; private set; }

        public DisciplineReportDescription Description => new DisciplineReportDescription(discipline, key, Order);

        public int Order { get; }
    }
}