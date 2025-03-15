using WastingNoTime.Contacts.Adapter.SQLite.Types;
using WastingNoTime.Contacts.Domain.Entities;
using WastingNoTime.Contacts.Port.Outbound.Persistence;

namespace WastingNoTime.Contacts.Adapter.SQLite.Test;

public class ContactsRepositoryTest
{
    [Fact]
    public async Task Can_delete_a_contact()
    {
        // arrange
        var contact = new ContactModel { Id = Guid.NewGuid(), FirstName = "Albert", LastName = "Einstein", PhoneNumber = "2222-1111" };
        var id = contact.Id;

        var context = await GetContactContext();
        await context.AddAsync(contact);
        await context.SaveChangesAsync();

        // act
        var sut = new ContactRepository(context) as IDeleteContact;
        await sut.DeleteAsync(new Contact.ContactId(id));

        // assert
        Assert.Empty(context.Contacts.Where(c => c.Id == id));
    }

    [Fact]
    public async Task Can_create_a_contact()
    {
        // arrange
        var expected = new ContactModel {Id = Guid.NewGuid(), FirstName = "Albert", LastName = "Einstein", PhoneNumber = "2222-1111" };
        var context = await GetContactContext();

        // act
        var sut = new ContactRepository(context) as ISaveContact;
        await sut.SaveAsync(new Contact(expected.FirstName, expected.LastName, expected.PhoneNumber){Id = new Contact.ContactId(expected.Id)});

        // assert
        Assert.Single(context.Contacts, c => c.Id == expected.Id);
        
        var actual = await context.Contacts.FindAsync(expected.Id);
        Assert.NotNull(actual);
        Assert.Equal(expected.FirstName, actual.FirstName);
        Assert.Equal(expected.LastName, actual.LastName);
        Assert.Equal(expected.PhoneNumber, actual.PhoneNumber);

    }


    [Fact]
    public async Task Can_update_a_contact()
    {
        // arrange
        var contact = new ContactModel {Id=Guid.NewGuid(),  FirstName = "Albert", LastName = "Einstein", PhoneNumber = "2222-1111" };
        var expected = new ContactModel
            { Id = contact.Id, FirstName = "Ulbert", LastName = "Oinstein", PhoneNumber = "3333-4444" };

        var context = await GetContactContext();
        await context.AddAsync(contact);
        await context.SaveChangesAsync();

        // act
        var sut = new ContactRepository(context) as IUpdateContact;
        await sut.UpdateAsync(new Contact(expected.FirstName, expected.LastName, expected.PhoneNumber)
            { Id = new Contact.ContactId(expected.Id) });

        // assert 
        Assert.Single(context.Contacts, c => c.Id == expected.Id);
        
        var actual = await context.Contacts.FindAsync(expected.Id);
        Assert.NotNull(actual);
        Assert.Equal(expected.FirstName, actual.FirstName);
        Assert.Equal(expected.LastName, actual.LastName);
        Assert.Equal(expected.PhoneNumber, actual.PhoneNumber);
    }

    private static Task<ContactContext> GetContactContext() =>
        new TestDbContextFactory().CreateContextAsync();
}