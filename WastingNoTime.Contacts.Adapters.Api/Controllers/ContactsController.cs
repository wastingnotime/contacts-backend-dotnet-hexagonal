using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WastingNoTime.Contacts.Adapters.Api.Contracts;
using WastingNoTime.Contacts.Application.Queries;
using WastingNoTime.Contacts.Domain.Exceptions;
using WastingNoTime.Contacts.Inbound.UseCases;

namespace WastingNoTime.Contacts.Adapters.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly ILogger<ContactsController> _logger;
    private readonly ICreateContactUseCase _createContactUseCase;
    private readonly IDeleteContactUseCase _deleteContactUseCase;
    private readonly IUpdateContactUseCase _updateContactUseCase;
    private readonly IContactQuery _contactQuery;

    public ContactsController(
        ILogger<ContactsController> logger,
        ICreateContactUseCase createContactUseCase,
        IDeleteContactUseCase deleteContactUseCase,
        IUpdateContactUseCase updateContactUseCase, 
        IContactQuery contactQuery)
    {
        _logger = logger;
        _createContactUseCase = createContactUseCase;
        _deleteContactUseCase = deleteContactUseCase;
        _updateContactUseCase = updateContactUseCase;
        _contactQuery = contactQuery;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ContactResource>> Get() =>
        _contactQuery
            .AsQueryable()
            .Select(item => new ContactResource
            {
                Id = item.Id.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                PhoneNumber = item.PhoneNumber
            })
            .ToList();

    [HttpGet("{id:guid:required}")]
    public ActionResult<ContactResource> Get(Guid id)
    {
        var item = _contactQuery.AsQueryable().FirstOrDefault(c => c.Id.Id == id);
        return item == null
            ? NotFound()
            : new ContactResource
            {
                Id = item.Id.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                PhoneNumber = item.PhoneNumber
            };
    }

    [HttpPost]
    public async Task<IActionResult> Post(ContactResource value)
    {
        try
        {
            //resource to cmd
            var output = await _createContactUseCase
                .Execute(new ICreateContactUseCase.CreateContactCommand(value.FirstName, value.LastName,
                    value.PhoneNumber));

            value.Id = output.Id.Id;
        }
        catch (ValidationException ve)
        {
            //400
            //     return BadRequest();   
        }
        catch (Exception e)
        {
            //500
            _logger.LogError(e, "Error creating contact");
            throw;
        }
        return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
    }

    [HttpPut("{id:guid:required}")]
    public async Task<IActionResult> Update(ContactResource value, Guid id)
    {
        try
        {
            await _updateContactUseCase
                .Execute(new IUpdateContactUseCase.UpdateContactCommand(id, value.FirstName, value.LastName,
                    value.PhoneNumber));
        }
        catch (ValidationException ve)
        {
            //400
            //     return BadRequest();   
        }
        catch (NotFoundException)
        {
            //404
            return NotFound();
        }
        catch (Exception e)
        {
            //500
            _logger.LogError(e, "Error updating contact");
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id:guid:required}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteContactUseCase.Execute(new IDeleteContactUseCase.DeleteContactCommand(id));
        }
        catch (ValidationException ve)
        {
            //400
            //     return BadRequest();   
        }
        catch (NotFoundException)
        {
            //404
            return NotFound();
        }
        catch (Exception e)
        {
            //500
            _logger.LogError(e, "Error deleting contact");
            throw;
        }

        return NoContent();
    }
}