using System.ComponentModel.DataAnnotations;

namespace WastingNoTime.Contacts.Port.Inbound.UseCases;

public interface IDeleteContactUseCase
{
    public Task Execute(Command cmd);
    
    public class Command(Guid id)
    {
        [Required (ErrorMessage = "{0} is a mandatory field")]
        public Guid Id{ get; } = id;
    }
}