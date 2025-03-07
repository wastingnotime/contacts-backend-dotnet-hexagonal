using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WastingNoTime.Contacts.Adapter.SQLite.DependencyInjection;
using WastingNoTime.Contacts.Application.Commands;
using WastingNoTime.Contacts.Port.Inbound.UseCases;

namespace WastingNoTime.Contacts.Startup.DependencyInjection;

public static class ContactsServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddScoped<ICreateContactUseCase, ContactService>()
            .AddScoped<IUpdateContactUseCase, ContactService>()
            .AddScoped<IDeleteContactUseCase, ContactService>()
            .AddSqLite(configuration);
    }
}