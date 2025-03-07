using System.ComponentModel.DataAnnotations;

namespace WastingNoTime.Contacts.Port.Inbound.UseCases;

public interface IUpdateContactUseCase
{
    public Task Execute(Command contact);
    
    public class Command(Guid id, string firstName, string lastName, string phoneNumber)
    {
        [Required (ErrorMessage = "{0} is a mandatory field")]
        public Guid Id{ get; set; } = id;
        [Required (ErrorMessage = "{0} is a mandatory field")]
        public string FirstName { get; set; } = firstName;
        [Required (ErrorMessage = "{0} is a mandatory field")]
        public string LastName { get; set; } = lastName;
        [Required (ErrorMessage = "{0} is a mandatory field")]
        public string PhoneNumber { get; set; } = phoneNumber;
    }
}