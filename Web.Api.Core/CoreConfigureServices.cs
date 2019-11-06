using Microsoft.Extensions.DependencyInjection;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Core.Interfaces.UseCases.QuoteRequest;
using Web.Api.Core.UseCases;
using Web.Api.Core.UseCases.QuoteRequest;

namespace Web.Api.Core
{
    public static class CoreConfigureServices
    {
        public static void MapCoreServices(this IServiceCollection services)
        {
            services.AddTransient<IUserRegisterUseCase, RegisterUserUseCase>();
            services.AddTransient<IUserLoginUseCase, LoginUserUseCase>();
            services.AddTransient<IFileUploadUseCase, FileUploadUseCase>();
            services.AddTransient<IFileFetchUseCase, FileFetchUseCase>();
            services.AddTransient<IFileFetchAllUseCase, FileFetchAllUseCase>();
            services.AddTransient<IHouseQuoteRequestCreateUseCase, HouseQuoteRequestUseCase>();
            services.AddTransient<IHouseQuoteRequestGetQuotesRequestUseCase, HouseQuoteGetAllRequestUseCase>();
            services.AddTransient<IFinancialCapacityFindUseCase, FindFinancialCapacityUseCase>();
            services.AddTransient<IFinancialCapacityRegisterUseCase, RegisterFinancialCapacityUseCase>();
        }
    }
}
