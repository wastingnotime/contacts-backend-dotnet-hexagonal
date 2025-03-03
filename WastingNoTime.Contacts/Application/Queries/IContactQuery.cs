using WastingNoTime.Contacts.Domain.Entities;

namespace WastingNoTime.Contacts.Application.Queries;

public interface IContactQuery
{
    IQueryable<Contact> AsQueryable();
}

