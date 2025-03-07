using WastingNoTime.Contacts.Domain.Entities;

namespace WastingNoTime.Contacts.Port.Outbound.Persistence;

public interface IExistsContact {
    Task<bool> ExistsAsync(Contact.ContactId id);
}