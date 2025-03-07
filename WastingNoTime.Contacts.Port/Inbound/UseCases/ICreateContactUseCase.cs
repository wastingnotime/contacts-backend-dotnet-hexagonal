using System.ComponentModel.DataAnnotations;
using WastingNoTime.Contacts.Domain.Entities;

namespace WastingNoTime.Contacts.Port.Inbound.UseCases;

public interface ICreateContactUseCase
{
    public Task<Result> Execute(Command contact);
    
    public class Result(Contact.ContactId contactId)
    {
        public Contact.ContactId Id { get; } = contactId;
    }

    public class Command(string firstName, string lastName, string phoneNumber)
    {
        [Required (ErrorMessage = "{0} is a mandatory field")]
        public string FirstName { get; } = firstName;
        [Required (ErrorMessage = "{0} is a mandatory field")]
        public string LastName { get; } = lastName;
        [Required (ErrorMessage = "{0} is a mandatory field")]
        public string PhoneNumber { get; } = phoneNumber;
    }
}