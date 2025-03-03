namespace WastingNoTime.Contacts.Inbound.UseCases;

public interface IUpdateContactUseCase
{
    public Task Execute(UpdateContactCommand contact);
    
    //command
    public class UpdateContactCommand(Guid id, string firstName, string lastName, string phoneNumber)
    {
        //TODO: try AOP
        //this.validateSelf();

        public Guid Id{ get; set; } = id;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public string PhoneNumber { get; set; } = phoneNumber;
        
    }
    
    public class UpdateContactCommandValidator
    {
        public IEnumerable<string> IsValid(UpdateContactCommand command)
        {
            return [];
        }
    }
}