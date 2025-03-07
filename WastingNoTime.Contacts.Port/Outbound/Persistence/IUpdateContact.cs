using WastingNoTime.Contacts.Domain.Entities;

namespace WastingNoTime.Contacts.Port.Outbound.Persistence;

public interface IUpdateContact {
    Task  UpdateAsync(Contact contact);
}