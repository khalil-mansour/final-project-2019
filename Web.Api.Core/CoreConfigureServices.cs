using Microsoft.Extensions.DependencyInjection;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Core.Interfaces.UseCases.File;
using Web.Api.Core.UseCases;
using Web.Api.Core.UseCases.File;

namespace Web.Api.Core
{
    public static class CoreConfigureServices
    {

        public static void MapCoreServices(this IServiceCollection services)
        {
            //user
            services.AddTransient<IUserRegisterUseCase, UserRegisterUseCase>();
            services.AddTransient<IUserLoginUseCase, UserLoginUseCase>();

            //file
            services.AddTransient<IFileUploadUseCase, FileUploadUseCase>();
            services.AddTransient<IFileFetchUseCase, FileFetchUseCase>();
            services.AddTransient<IFileFetchAllUseCase, FileFetchAllUseCase>();
            services.AddTransient<IFileDeleteUseCase, FileDeleteUseCase>();
            services.AddTransient<IFinancialCapacityFindUseCase, FindFinancialCapacityUseCase>();
            services.AddTransient<IFinancialCapacityRegisterUseCase, RegisterFinancialCapacityUseCase>();
        }
    }
}
