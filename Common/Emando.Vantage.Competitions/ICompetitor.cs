using System;

namespace Emando.Vantage.Competitions
{
    public interface ICompetitor
    {
        Guid Id { get; }

        Gender Gender { get; }
    }
}