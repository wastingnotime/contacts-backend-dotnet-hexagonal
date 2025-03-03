using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WastingNoTime.Contacts.Application.Queries;
using WastingNoTime.Contacts.Outbound.Persistence;


namespace WastingNoTime.Contacts.Adapters.SQLite.DependencyInjection;


public static class SQLiteServiceCollectionExtensions
{
    public static IServiceCollection AddSQLite(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddScoped<ISaveContact , ContactRepository>()
            .AddScoped<IUpdateContact, ContactRepository>()
            .AddScoped<IDeleteContact, ContactRepository>()
            .AddScoped<IExistsContact, ContactRepository>()
            .AddScoped<IGetContact,ContactRepository>()
            .AddScoped<IContactQuery, ContactQuery>()
            .AddDbContext<ContactContext>(options => options.UseSqlite(configuration.GetConnectionString("contactsDb")));
    }
}