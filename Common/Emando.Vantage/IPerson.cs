using System;

namespace Emando.Vantage
{
    public interface IPerson
    {
        Guid Id { get; }

        Name Name { get; }

        string Email { get; }

        string Phone { get; }

        Address Address { get; }

        Gender Gender { get; }

        string NationalityCode { get; }

        DateTime BirthDate { get; }

        string Iban { get; }
    }
}