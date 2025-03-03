namespace WastingNoTime.Contacts.Domain.Entities;

public class Contact
{
    public ContactId Id { get; set; } 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }

    public Contact(string firstName, string lastName, string phoneNumber)
    {
        Id = new ContactId();
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
    }
    
    public class ContactId 
    {
        public ContactId (){}

        public ContactId(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}