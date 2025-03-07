namespace WastingNoTime.Contacts.Port.Inbound.Queries;

public interface IContactQuery
{
    IQueryable<ContactModel> AsQueryable();

    public class ContactModel()
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
   
    }
}