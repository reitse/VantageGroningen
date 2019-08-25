using System;

namespace Emando.Vantage.Components.Adapters
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class AdapterAttribute : Attribute
    {
        private readonly string name;
        private readonly int sortOrder;

        public AdapterAttribute(string name, int sortOrder = 0)
        {
            this.name = name;
            this.sortOrder = sortOrder;
        }

        public string Name => name;

        public int SortOrder => sortOrder;
    }
}