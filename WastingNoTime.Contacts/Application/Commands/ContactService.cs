using System.ComponentModel.DataAnnotations;
using WastingNoTime.Contacts.Domain.Entities;
using WastingNoTime.Contacts.Domain.Exceptions;
using WastingNoTime.Contacts.Inbound.UseCases;
using WastingNoTime.Contacts.Outbound.Persistence;

namespace WastingNoTime.Contacts.Application.Commands;

public class ContactService : ICreateContactUseCase, IUpdateContactUseCase , IDeleteContactUseCase
{
    private readonly ISaveContact _saveContact;
    private readonly IUpdateContact _updateContact;
    private readonly IDeleteContact _deleteContact;
    private readonly IExistsContact _existsContact;
    private readonly IGetContact _getContact;

    public ContactService(
        ISaveContact saveContact, 
        IUpdateContact updateContact, 
        IDeleteContact deleteContact, 
        IExistsContact existsContact, 
        IGetContact getContact)
    {
        _saveContact = saveContact;
        _updateContact = updateContact;
        _deleteContact = deleteContact;
        _existsContact = existsContact;
        _getContact = getContact;
    }

    public async Task<ICreateContactUseCase.CreateContactResult> Execute(ICreateContactUseCase.CreateContactCommand command)
    {
        //validate cmd
        var msg = new ICreateContactUseCase.CreateContactCommandValidator().IsValid(command);
        if (msg.Any())
            throw new ValidationException();
        
        //cmd to entity
        var entity = new Contact(command.FirstName, command.LastName,command.PhoneNumber);
        
        //persistence
        await _saveContact.SaveAsync(entity);
        
        //output
        return new ICreateContactUseCase.CreateContactResult(entity.Id);
    }

    public async Task Execute(IUpdateContactUseCase.UpdateContactCommand command)
    {
        //validate cmd
        var msg = new IUpdateContactUseCase.UpdateContactCommandValidator().IsValid(command);
        if (msg.Any())
            throw new ValidationException();

        //verify existing
        if (! await _existsContact.ExistsAsync( new Contact.ContactId(command.Id)))
            throw new NotFoundException();

        //cmd to entity
        var currentContact  = new Contact(command.FirstName, command.LastName,command.PhoneNumber);
        
        //persistence
        await _updateContact.UpdateAsync(currentContact);
        
        //output
        //no error
    }

    public async Task Execute(IDeleteContactUseCase.DeleteContactCommand command)
    {
        //validate cmd
        var msg = new IDeleteContactUseCase.DeleteContactCommandValidator().IsValid(command);
        if (msg.Any())
            throw new ValidationException();
        
        //retrieve existing
        var currentEntity = await _getContact.GetAsync(new Contact.ContactId(command.Id));
        if (currentEntity is null)
             throw new NotFoundException();
        
        //persistence
        await _deleteContact.DeleteAsync(currentEntity);
        
        //output
        //no error
    }
}

