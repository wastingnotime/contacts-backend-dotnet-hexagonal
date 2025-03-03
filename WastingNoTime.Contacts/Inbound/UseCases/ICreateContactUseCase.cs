using WastingNoTime.Contacts.Domain.Entities;

namespace WastingNoTime.Contacts.Inbound.UseCases;

public interface ICreateContactUseCase
{
    public Task<CreateContactResult> Execute(CreateContactCommand contact);
    
    //response command
    public class CreateContactResult(Contact.ContactId contactId)
    {
        public Contact.ContactId Id { get; set; } = contactId;
    }

    //command
    public class CreateContactCommand(string firstName, string lastName, string phoneNumber)
    {
        //TODO: try AOP
        //this.validateSelf();

        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public string PhoneNumber { get; set; } = phoneNumber;
    }

    public class CreateContactCommandValidator
    {
        public IEnumerable<string> IsValid(CreateContactCommand command)
        {
            return [];
        }
    }
}