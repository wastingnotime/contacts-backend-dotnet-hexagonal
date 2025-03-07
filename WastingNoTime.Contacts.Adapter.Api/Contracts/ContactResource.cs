namespace WastingNoTime.Contacts.Adapter.Api.Contracts;

public class ContactResource
{
    public Guid Id { get; set; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string PhoneNumber { get; init; }
}