using Microsoft.EntityFrameworkCore;
using WastingNoTime.Contacts.Adapters.SQLite.Contracts;
using WastingNoTime.Contacts.Domain.Entities;

namespace WastingNoTime.Contacts.Adapters.SQLite;

public class ContactContext : DbContext
{
    //dotnet ef migrations add initial --project WastingNoTime.Contacts.Adapters.SQLite
    //dotnet ef database update --project WastingNoTime.Contacts.Adapters.SQLite
    #region issue with migrations

    public ContactContext() 
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=../contacts.db;");
    }

    #endregion

    public ContactContext(DbContextOptions<ContactContext> options) : base(options)
    {
    }

    public DbSet<ContactModel> Contacts { get; protected set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactModel>(b =>
        {
            //b.ToTable("Contacts");
            b.HasKey(e => e.Id);
            b.Property(e => e.Id);
            b.Property(e => e.FirstName);
            b.Property(e => e.LastName);
            b.Property(e => e.PhoneNumber);
        });
    }
}