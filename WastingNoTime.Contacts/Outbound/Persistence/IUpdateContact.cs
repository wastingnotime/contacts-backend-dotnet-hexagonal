using WastingNoTime.Contacts.Domain;
using WastingNoTime.Contacts.Domain.Entities;

namespace WastingNoTime.Contacts.Outbound.Persistence;

public interface IUpdateContact {
    Task  UpdateAsync(Contact contact);
}