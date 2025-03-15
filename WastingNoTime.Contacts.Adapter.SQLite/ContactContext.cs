using Microsoft.EntityFrameworkCore;
using WastingNoTime.Contacts.Adapter.SQLite.Types;

namespace WastingNoTime.Contacts.Adapter.SQLite;

public class ContactContext : DbContext
{
    #region issue with migrations
    // this code is just about running migration
    // it is not called by the application itself

    // do not forget: to create the first migration  
    // dotnet ef migrations add initial --project WastingNoTime.Contacts.Domain.Adapters.SQLite
    
    // after that just update is needed
    // dotnet ef database update --project WastingNoTime.Contacts.Domain.Adapters.SQLite

    public ContactContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
            return;

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
            b.HasKey(e => e.Id);
            b.Property(e => e.Id);
            b.Property(e => e.FirstName);
            b.Property(e => e.LastName);
            b.Property(e => e.PhoneNumber);
        });
    }
}