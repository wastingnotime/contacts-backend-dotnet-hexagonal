using WastingNoTime.Contacts.Domain;
using WastingNoTime.Contacts.Domain.Entities;

namespace WastingNoTime.Contacts.Outbound.Persistence;

public interface IExistsContact {
    Task<bool> ExistsAsync(Contact.ContactId id);
}