using Moq;
using WastingNoTime.Contacts.Application.Commands;
using WastingNoTime.Contacts.Domain.Entities;
using WastingNoTime.Contacts.Port.Inbound.UseCases;
using WastingNoTime.Contacts.Port.Outbound.Persistence;

namespace WastingNoTime.Contacts.Application.Test.Commands;

// for sake of simplicity only positive tests has been created
// on real cases we need to test undesirable scenarios
public class ContactServiceTest
{
    [Fact]
    public async Task Can_create_a_contact()
    {
        // arrange
        var contact = new Contact("Albert", "Einstein", "2222-1111");

        var saveContactMock = new Mock<ISaveContact>();
        saveContactMock
            .Setup(x => x.SaveAsync(It.IsAny<Contact>()))
            .Returns(Task.CompletedTask);

        var sut = new ContactService(saveContactMock.Object, null, null) as ICreateContactUseCase;

        // act
        var result =
            await sut.Execute(new ICreateContactUseCase.Command(contact.FirstName, contact.LastName,
                contact.PhoneNumber));

        // assert
        Assert.NotNull(result);
        Assert.NotNull(result.Id);
        Assert.IsType<Contact.ContactId>(result.Id);
        Assert.IsType<Guid>(result.Id.Id);
        Assert.NotEqual(Guid.Empty, result.Id.Id);
        saveContactMock.Verify(x=>x.SaveAsync(It.IsAny<Contact>()), Times.Once);
    }

    [Fact]
    public async Task Can_update_a_contact()
    {
        // arrange
        var contact = new Contact("Albert", "Einstein", "2222-1111");

        var updateContactMock = new Mock<IUpdateContact>();
        updateContactMock
            .Setup(x => x.UpdateAsync(It.IsAny<Contact>()))
            .Returns(Task.CompletedTask);

        var sut = new ContactService(null, updateContactMock.Object, null) as IUpdateContactUseCase;

        // act
        await sut.Execute(new IUpdateContactUseCase
                .Command(contact.Id.Id, contact.FirstName, contact.LastName, contact.PhoneNumber));

        // assert
        updateContactMock.Verify(x=>x.UpdateAsync(It.IsAny<Contact>()), Times.Once);
    }
    
    [Fact]
    public async Task Can_delete_a_contact()
    {
        var contact = new Contact("Albert", "Einstein", "2222-1111");

        var deleteContactMock = new Mock<IDeleteContact>();
        deleteContactMock
            .Setup(x => x.DeleteAsync(It.IsAny<Contact.ContactId>()))
            .Returns(Task.CompletedTask);

        var sut = new ContactService(null, null, deleteContactMock.Object) as IDeleteContactUseCase;

        // act
        await sut.Execute(new IDeleteContactUseCase
            .Command(contact.Id.Id));

        // assert
        deleteContactMock.Verify(x=>x.DeleteAsync(It.IsAny<Contact.ContactId>()), Times.Once);        
    }    
}