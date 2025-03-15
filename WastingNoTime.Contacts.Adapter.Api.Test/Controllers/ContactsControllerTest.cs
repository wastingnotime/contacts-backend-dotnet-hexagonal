using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WastingNoTime.Contacts.Adapter.Api.Contracts;
using WastingNoTime.Contacts.Adapter.Api.Controllers;
using WastingNoTime.Contacts.Domain.Entities;
using WastingNoTime.Contacts.Port.Inbound.Queries;
using WastingNoTime.Contacts.Port.Inbound.UseCases;

namespace WastingNoTime.Contacts.Adapter.Api.Test.Controllers;

public class ContactsControllerTest
{
    [Fact]
    public void Can_get_all_contacts()
    {
        // arrange
        var queryable = new List<IContactQuery.ContactModel>
        {
            new() { Id = Guid.NewGuid(), FirstName = "Albert", LastName = "Einstein", PhoneNumber = "2222-1111" },
            new() { Id = Guid.NewGuid(), FirstName = "Marie", LastName = "Curie", PhoneNumber = "1111-1111" }
        }.AsQueryable();

        var mock = new Mock<IContactQuery>();
        mock
            .Setup(x => x.AsQueryable())
            .Returns(queryable);

        // act
        var result = new ContactsController(
                new Mock<ILogger<ContactsController>>().Object,
                null!,
                null!,
                null!,
                mock.Object)
            .Get();

        // assert
        var actual = result.Value;

        Assert.NotNull(actual);
        Assert.IsAssignableFrom<IEnumerable<ContactResource>>(actual);
        Assert.Equal(2, actual.Count());

        var albert = actual.First(x => x.FirstName == "Albert");
        Assert.NotNull(albert);
        Assert.Equal("Albert", albert.FirstName);

        var marie = actual.First(x => x.FirstName == "Marie");
        Assert.NotNull(marie);
        Assert.Equal("Marie", marie.FirstName);
    }

    [Fact]
    public void Can_get_one_contact()
    {
        // arrange
        var id = Guid.NewGuid();
        var expected = new IContactQuery.ContactModel
            { Id = id, FirstName = "Albert", LastName = "Einstein", PhoneNumber = "2222-1111" };
        var queryable = new List<IContactQuery.ContactModel>
        {
            expected,
            new() { Id = Guid.NewGuid(), FirstName = "Marie", LastName = "Curie", PhoneNumber = "1111-1111" }
        }.AsQueryable();

        var contactQueryMock = new Mock<IContactQuery>();
        contactQueryMock
            .Setup(x => x.AsQueryable())
            .Returns(queryable);

        // act
        var result = new ContactsController(
                new Mock<ILogger<ContactsController>>().Object,
                null!,
                null!,
                null!,
                contactQueryMock.Object)
            .Get(id);

        // assert
        Assert.NotNull(result);
        var actual = result.Value;
        Assert.NotNull(actual);
        Assert.IsType<ContactResource>(actual);
        Assert.Equal(expected.FirstName, actual.FirstName);
        Assert.Equal(expected.LastName, actual.LastName);
        Assert.Equal(expected.PhoneNumber, actual.PhoneNumber);
    }

    [Fact]
    public async Task Can_delete_a_contact()
    {
        var id = Guid.NewGuid();

        var mock = new Mock<IDeleteContactUseCase>();
        mock
            .Setup(x => x.Execute(It.Is<IDeleteContactUseCase.Command>(c => c.Id == id)))
            .Returns(Task.CompletedTask);

        // act
        var result = await new ContactsController(
                new Mock<ILogger<ContactsController>>().Object,
                null!,
                mock.Object,
                null!,
                null!)
            .Delete(id);

        // assert
        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);

        mock.Verify(x => x.Execute(It.Is<IDeleteContactUseCase.Command>(c => c.Id == id)), Times.Once());
    }

    [Fact]
    public async Task Can_create_a_contact()
    {
        // arrange
        var id = Guid.NewGuid();
        var mock = new Mock<ICreateContactUseCase>();
        mock
            .Setup(x => x.Execute(It.IsAny<ICreateContactUseCase.Command>()))
            .Returns(Task.FromResult(new ICreateContactUseCase.Result(new Contact.ContactId(id))));

        // act
        var result = await new ContactsController(new Mock<ILogger<ContactsController>>().Object,
                mock.Object,
                null!,
                null!,
                null!)
            .Post(new ContactResource { FirstName = "Albert", LastName = "Einstein", PhoneNumber = "2222-1111" });

        // assert
        Assert.IsType<CreatedAtActionResult>(result);
        var derivedResult = result as CreatedAtActionResult;
        Assert.NotNull(derivedResult);
        Assert.IsType<ContactResource>(derivedResult.Value);
        var actual = (ContactResource)derivedResult.Value;
        Assert.Equal(id, actual.Id);
        Assert.Equal("Albert", actual.FirstName);
        Assert.Equal("Einstein", actual.LastName);
        Assert.Equal("2222-1111", actual.PhoneNumber);
    }

    [Fact]
    public async Task Can_update_a_contact()
    {
        // arrange
        var id = Guid.NewGuid();
        var mock = new Mock<IUpdateContactUseCase>();
        mock
            .Setup(x => x.Execute(It.IsAny<IUpdateContactUseCase.Command>()))
            .Returns(Task.CompletedTask);

        // act
        var result = await new ContactsController(
                new Mock<ILogger<ContactsController>>().Object,
                null!,
                null!,
                mock.Object,
                null!)
            .Update(
                new ContactResource { Id = id, FirstName = "Albert", LastName = "Einstein", PhoneNumber = "2222-1111" },
                id);

        // assert
        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);
        mock.Verify(x => x.Execute(It.Is<IUpdateContactUseCase.Command>(c => c.Id == id)), Times.Once());
    }
}