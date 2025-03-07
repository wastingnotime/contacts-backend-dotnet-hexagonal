using Microsoft.EntityFrameworkCore;
using WastingNoTime.Contacts.Adapter.SQLite.Types;
using WastingNoTime.Contacts.Domain.Entities;
using WastingNoTime.Contacts.Domain.Exceptions;
using WastingNoTime.Contacts.Port.Outbound.Persistence;

namespace WastingNoTime.Contacts.Adapter.SQLite;

public class ContactRepository: ISaveContact , IUpdateContact, IDeleteContact, IExistsContact, IGetContact {
    
    private readonly ContactContext _context ;
    
    public ContactRepository(ContactContext context)
    {
        _context = context;
    }
    public Task SaveAsync(Contact contact)
    {
        var model = new ContactModel{Id = contact.Id.Id, FirstName = contact.FirstName, LastName = contact.LastName, PhoneNumber = contact.PhoneNumber};
        _context.Contacts.Add(model);
        return _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Contact contact)
    {
        var current  = await _context.Contacts.FindAsync(contact.Id.Id);
        if (current == null)
            throw new NotFoundException();
        
        current.FirstName = contact.FirstName;
        current.LastName = contact.LastName;
        current.PhoneNumber = contact.PhoneNumber;

        _context.Contacts.Update(current);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Contact.ContactId id)
    {
        var current  = await _context.Contacts.FindAsync(id.Id);
        if (current == null)
            throw new NotFoundException();

        _context.Contacts.Remove(current);
        
        await _context.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(Contact.ContactId id)
    {
        return _context
            .Contacts
            .AnyAsync(c => c.Id == id.Id);
    }

    public Task<Contact> GetAsync(Contact.ContactId id)
    {
        return _context
            .Contacts
            .Select(c=> new Contact(c.FirstName, c.LastName,c.PhoneNumber){Id = new Contact.ContactId( c.Id)})
            .FirstOrDefaultAsync(c => c.Id.Id == id.Id);
    }
}