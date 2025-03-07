using WastingNoTime.Contacts.Domain.Entities;

namespace WastingNoTime.Contacts.Port.Outbound.Persistence;

public interface IDeleteContact {
    Task DeleteAsync(Contact.ContactId id);
}