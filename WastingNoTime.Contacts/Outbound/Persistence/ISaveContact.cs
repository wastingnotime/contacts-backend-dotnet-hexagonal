using WastingNoTime.Contacts.Domain;
using WastingNoTime.Contacts.Domain.Entities;

namespace WastingNoTime.Contacts.Outbound.Persistence;

public interface ISaveContact {
    Task SaveAsync(Contact contact);
}