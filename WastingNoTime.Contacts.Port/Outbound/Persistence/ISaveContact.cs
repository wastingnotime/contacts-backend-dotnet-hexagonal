using WastingNoTime.Contacts.Domain.Entities;

namespace WastingNoTime.Contacts.Port.Outbound.Persistence;

public interface ISaveContact {
    Task SaveAsync(Contact contact);
}