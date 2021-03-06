using Microsoft.Extensions.DependencyInjection;
using Web.Api.Core.Interfaces.Gateways.Repositories;

using Web.Api.Infrastructure.Repositories;

namespace Web.Api.Infrastructure
{
    public static class InfrastructureConfigureServices
    {
        public static void MapInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<IQuoteRequestRepository, QuoteRequestRepository>();
            services.AddTransient<IFinancialCapacityRepository, FinancialCapacityRepository>();
            services.AddTransient<IOfferRepository, OfferRepository>();
            services.AddTransient<IChatRepository, ChatRepository>();
        }
    }
}
