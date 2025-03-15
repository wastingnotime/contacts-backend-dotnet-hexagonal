namespace WastingNoTime.Contacts.Domain.Entities;

public class Contact
{
    public ContactId Id { get; init; }
    public string FirstName { get; }
    public string LastName { get; }
    public string PhoneNumber { get; }

    public Contact(string firstName, string lastName, string phoneNumber)
    {
        Id = new ContactId();
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
    }

    public class ContactId
    {
        public ContactId()
        {
        }

        public ContactId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; } = Guid.NewGuid();
    }
}