using Microsoft.Extensions.DependencyInjection;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Core.Interfaces.UseCases.Chat;
using Web.Api.Core.Interfaces.UseCases.File;
using Web.Api.Core.Interfaces.UseCases.Offer;
using Web.Api.Core.Interfaces.UseCases.QuoteRequest;
using Web.Api.Core.Interfaces.UseCases.User;
using Web.Api.Core.UseCases;
using Web.Api.Core.UseCases.Chat;
using Web.Api.Core.UseCases.File;
using Web.Api.Core.UseCases.Offer;
using Web.Api.Core.UseCases.QuoteRequest;
using Web.Api.Core.UseCases.User;

namespace Web.Api.Core
{
    public static class CoreConfigureServices
    {

        public static void MapCoreServices(this IServiceCollection services)
        {
            // user
            services.AddTransient<IUserRegisterUseCase, UserRegisterUseCase>();
            services.AddTransient<IUserLoginUseCase, UserLoginUseCase>();
            services.AddTransient<IUserUpdateUseCase, UserUpdateUseCase>();
            services.AddTransient<IUserProfileUpdateUseCase, UserProfileUpdateUseCase>();
            services.AddTransient<IUserFetchUseCase, UserFetchUseCase>();
            services.AddTransient<IUserProfileFetchUseCase, UserProfileFetchUseCase>();

            // file
            services.AddTransient<IFileUploadUseCase, FileUploadUseCase>();
            services.AddTransient<IFileFetchUseCase, FileFetchUseCase>();
            services.AddTransient<IFileFetchAllUseCase, FileFetchAllUseCase>();
            services.AddTransient<IFileDeleteUseCase, FileDeleteUseCase>();

            // financial capacity
            services.AddTransient<IFinancialCapacityFindUseCase, FindFinancialCapacityUseCase>();
            services.AddTransient<IFinancialCapacityRegisterUseCase, RegisterFinancialCapacityUseCase>();

            // quote request
            services.AddTransient<IHouseQuoteRequestCreateUseCase, HouseQuoteRequestCreateUseCase>();
            services.AddTransient<IHouseQuoteRequestDeleteUseCase, HouseQuoteRequestDeleteUseCase>();
            services.AddTransient<IHouseQuoteRequestFetchAllUseCase, HouseQuoteRequestFetchAllUseCase>();
            services.AddTransient<IHouseQuoteRequestGetDetailRequestUseCase, HouseQuoteRequestGetDetailUseCase>();
            services.AddTransient<IHouseQuoteRequestUpdateUseCase, HouseQuoteRequestUpdateUseCase>();

            // offer
            services.AddTransient<IOfferCreateUseCase, OfferCreateUseCase>();
            services.AddTransient<IOfferFetchUseCase, OfferFetchUseCase>();
            services.AddTransient<IOfferFetchAllUseCase, OfferFetchAllUseCase>();
            services.AddTransient<IOfferDeleteUseCase, OfferDeleteUseCase>();
            services.AddTransient<IOfferFetchAllByReqUseCase, OfferFetchAllByReqUseCase>();
            services.AddTransient<IOfferUpdateUseCase, OfferUpdateUseCase>();

            // chat
            services.AddTransient<IChatSendUseCase, ChatSendUseCase>();
            services.AddTransient<IChatFetchUseCase, ChatFetchUseCase>();
        }
    }
}
