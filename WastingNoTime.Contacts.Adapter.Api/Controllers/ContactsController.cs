using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WastingNoTime.Contacts.Adapter.Api.Contracts;
using WastingNoTime.Contacts.Domain.Exceptions;
using WastingNoTime.Contacts.Port.Inbound.Queries;
using WastingNoTime.Contacts.Port.Inbound.UseCases;

namespace WastingNoTime.Contacts.Adapter.Api.Controllers;

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
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                PhoneNumber = item.PhoneNumber
            })
            .ToList();

    [HttpGet("{id:guid:required}")]
    public ActionResult<ContactResource> Get(Guid id)
    {
        var item = _contactQuery.AsQueryable().FirstOrDefault(c => c.Id == id);
        return item == null
            ? NotFound()
            : new ContactResource
            {
                Id = item.Id,
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
                .Execute(new ICreateContactUseCase.Command(value.FirstName, value.LastName, value.PhoneNumber));

            value.Id = output.Id.Id;
        }
        catch (ValidationException ve)
        {
            return BadRequest(new
            {
                Type = "WastingNoTime.Contacts.Domain",
                Title = "One or more validation errors occurred.",
                Status = 400,
                Errors = new[] { ve.ValidationResult.ErrorMessage }
            });
        }

        return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
    }

    [HttpPut("{id:guid:required}")]
    public async Task<IActionResult> Update(ContactResource value, Guid id)
    {
        try
        {
            await _updateContactUseCase
                .Execute(new IUpdateContactUseCase.Command(id, value.FirstName, value.LastName, value.PhoneNumber));
        }
        catch (ValidationException ve)
        {
            return BadRequest(new
            {
                Type = "WastingNoTime.Contacts.Domain",
                Title = "One or more validation errors occurred.",
                Status = 400,
                Errors = new[] { ve.ValidationResult.ErrorMessage }
            });
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:guid:required}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteContactUseCase.Execute(new IDeleteContactUseCase.Command(id));
        }
        catch (ValidationException ve)
        {
            return BadRequest(new
            {
                Type = "WastingNoTime.Contacts.Domain",
                Title = "One or more validation errors occurred.",
                Status = 400,
                Errors = new[] { ve.ValidationResult.ErrorMessage }
            });
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}