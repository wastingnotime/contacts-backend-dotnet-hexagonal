using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WastingNoTime.Contacts.Port.Inbound.Queries;
using WastingNoTime.Contacts.Port.Outbound.Persistence;


namespace WastingNoTime.Contacts.Adapter.SQLite.DependencyInjection;

public static class SqLiteServiceCollectionExtensions
{
    public static IServiceCollection AddSqLite(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddScoped<ISaveContact, ContactRepository>()
            .AddScoped<IUpdateContact, ContactRepository>()
            .AddScoped<IDeleteContact, ContactRepository>()
            .AddScoped<IExistsContact, ContactRepository>()
            .AddScoped<IGetContact, ContactRepository>()
            .AddScoped<IContactQuery, ContactQuery>()
            .AddDbContext<ContactContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("contactsDb")));
    }
}