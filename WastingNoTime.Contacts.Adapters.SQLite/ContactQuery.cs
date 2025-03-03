using WastingNoTime.Contacts.Application.Queries;
using WastingNoTime.Contacts.Domain.Entities;

namespace WastingNoTime.Contacts.Adapters.SQLite;

public class ContactQuery:IContactQuery
{
    private readonly ContactContext _context ;
    
    public ContactQuery(ContactContext context)
    {
        _context = context;
    }
    public IQueryable<Contact> AsQueryable()
    {
        return _context.Contacts.Select(c=> new Contact(c.FirstName, c.LastName,c.PhoneNumber){Id = new Contact.ContactId( c.Id)});
    }
}