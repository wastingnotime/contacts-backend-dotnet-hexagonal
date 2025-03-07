// using WastingNoTime.Contacts.Domain.Entities;
// using WastingNoTime.Contacts.Port.Inbound.Queries;
//
// namespace WastingNoTime.Contacts.Application.Queries;
//
// public class ContactQuery : IContactQuery
// {
//     private readonly IContactQuery _contactQuery;
//
//     public ContactQuery(IContactQuery contactQuery)
//     {
//         _contactQuery = contactQuery;
//     }
//
//     public IQueryable<IContactQuery.ContactModel> AsQueryable() => _contactQuery.AsQueryable();
// }