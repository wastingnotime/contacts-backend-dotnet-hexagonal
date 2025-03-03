using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WastingNoTime.Contacts.Adapters.SQLite.DependencyInjection;
using WastingNoTime.Contacts.Application.Commands;
using WastingNoTime.Contacts.Inbound.UseCases;

namespace WastingNoTime.Contacts.Startup.DependencyInjection;

public static class ContactsServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddScoped<ICreateContactUseCase, ContactService>()
            .AddScoped<IUpdateContactUseCase, ContactService>()
            .AddScoped<IDeleteContactUseCase, ContactService>()
            .AddSQLite(configuration);
    }
}