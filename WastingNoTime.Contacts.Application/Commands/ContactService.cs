using System.ComponentModel.DataAnnotations;
using WastingNoTime.Contacts.Domain.Entities;
using WastingNoTime.Contacts.Port.Inbound.UseCases;
using WastingNoTime.Contacts.Port.Outbound.Persistence;

namespace WastingNoTime.Contacts.Application.Commands;

public class ContactService : ICreateContactUseCase, IUpdateContactUseCase, IDeleteContactUseCase
{
    private readonly ISaveContact _saveContact;
    private readonly IUpdateContact _updateContact;
    private readonly IDeleteContact _deleteContact;

    public ContactService(
        ISaveContact saveContact,
        IUpdateContact updateContact,
        IDeleteContact deleteContact)
    {
        _saveContact = saveContact;
        _updateContact = updateContact;
        _deleteContact = deleteContact;
    }

    public async Task<ICreateContactUseCase.Result> Execute(ICreateContactUseCase.Command command)
    {
        //validate cmd
        Validator.ValidateObject(command, new ValidationContext(command));

        //cmd to entity
        var entity = new Contact(command.FirstName, command.LastName, command.PhoneNumber);

        //persistence
        await _saveContact.SaveAsync(entity);

        //output
        return new ICreateContactUseCase.Result(entity.Id);
    }

    public async Task Execute(IUpdateContactUseCase.Command command)
    {
        //validate cmd
        Validator.ValidateObject(command, new ValidationContext(command));

        //cmd to entity
        var entity = new Contact(command.FirstName, command.LastName, command.PhoneNumber)
            { Id = new Contact.ContactId(command.Id) };

        //persistence
        await _updateContact.UpdateAsync(entity);

        //output - just return
    }

    public async Task Execute(IDeleteContactUseCase.Command command)
    {
        //validate cmd
        Validator.ValidateObject(command, new ValidationContext(command));

        //cmd to entity
        var id = new Contact.ContactId(command.Id);

        //persistence
        await _deleteContact.DeleteAsync(id);

        //output - just return
    }
}