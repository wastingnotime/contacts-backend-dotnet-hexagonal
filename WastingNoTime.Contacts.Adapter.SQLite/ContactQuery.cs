using WastingNoTime.Contacts.Port.Inbound.Queries;

namespace WastingNoTime.Contacts.Adapter.SQLite;

public class ContactQuery : IContactQuery
{
    private readonly ContactContext _context;

    public ContactQuery(ContactContext context)
    {
        _context = context;
    }

    public IQueryable<IContactQuery.ContactModel> AsQueryable()
    {
        return _context
            .Contacts
            .Select(c => new IContactQuery.ContactModel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber
            });
    }
}