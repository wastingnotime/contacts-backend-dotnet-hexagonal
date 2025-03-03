using WastingNoTime.Contacts.Domain.Entities;

namespace WastingNoTime.Contacts.Test.Domain.Entities;

public class ContactTest
{
    
    [Fact]
    //GIVEN any firstname, lastname and phone
    //WHEN create method is called
    //THEN a new contact should be created
    //  AND a guid that represents it's ID should return
    public void Contact_construct()
    {
        //arrange
        var firstName = Guid.NewGuid().ToString();
        var lastName=Guid.NewGuid().ToString();
        var phoneNumber=Guid.NewGuid().ToString();
        
        //act
        var entity = new Contact(firstName, lastName,  phoneNumber);

        //assert
        Assert.NotNull(entity.Id);
        Assert.IsType<Contact.ContactId>(entity.Id);
        Assert.IsType<Guid>(entity.Id.Id);
        Assert.True(entity.FirstName == firstName);
        Assert.True(entity.LastName == lastName);
        Assert.True(entity.PhoneNumber == phoneNumber);
    }
}