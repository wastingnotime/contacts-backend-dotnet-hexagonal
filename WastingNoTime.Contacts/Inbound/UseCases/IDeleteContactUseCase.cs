namespace WastingNoTime.Contacts.Inbound.UseCases;

public interface IDeleteContactUseCase
{
    public Task Execute(DeleteContactCommand cmd);
    
    //command
    public class DeleteContactCommand(Guid id)
    {
        //TODO: try AOP
        //this.validateSelf();

        public Guid Id{ get; set; } = id;
    }
    
    public class DeleteContactCommandValidator
    {
        public IEnumerable<string> IsValid(DeleteContactCommand command)
        {
            return [];
        }
    }
}