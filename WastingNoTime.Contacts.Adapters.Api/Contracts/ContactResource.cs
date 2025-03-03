namespace WastingNoTime.Contacts.Adapters.Api.Contracts;

public class ContactResource
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
}