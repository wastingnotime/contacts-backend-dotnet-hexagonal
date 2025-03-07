using WastingNoTime.Contacts.Domain.Entities;

namespace WastingNoTime.Contacts.Port.Outbound.Persistence;

public interface IGetContact {
    Task<Contact> GetAsync(Contact.ContactId id);
}