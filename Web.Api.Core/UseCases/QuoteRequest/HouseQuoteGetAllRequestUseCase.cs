using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.GatewayResponses.Repositories.QuoteRequest;
using Web.Api.Core.Dto.UseCaseRequests.QuoteRequest;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases.QuoteRequest;

namespace Web.Api.Core.UseCases.QuoteRequest
{
    public sealed class HouseQuoteGetAllRequestUseCase: IHouseQuoteRequestGetQuotesRequestUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IQuoteRequestRepository _quoteRequestRepository;
        private readonly IUserRepository _userRepository;

        public HouseQuoteGetAllRequestUseCase(IQuoteRequestRepository quoteRequestReposiroty, IUserRepository userRepository) {
            _quoteRequestRepository = quoteRequestReposiroty;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(HouseQuoteRequestGetAllRequest message, IOutputPort<HouseQuoteGetAllRequestResponse> outputPort)
        {
            // return user to check his type
            var user = await _userRepository.FindById(message.UserId);

            // response object
            HouseQuoteRequestGetAllRepoResponse response;

            // if client, fetch his quote requests
            if (user.User.UserType == 1)
                response = await _quoteRequestRepository.GetAllQuoteForUser(message.UserId);
            // if broker, fetch all _available_ quote requests
            else
                response = await _quoteRequestRepository.GetAllQuotes();


            outputPort.Handle(response.Success ? new HouseQuoteGetAllRequestResponse(response.HouseQuoteRequests, true, null) : new HouseQuoteGetAllRequestResponse(new[] { new Error("Action Failed", "Enable to fetch house quote requests") }));

            if (!response.Success)
                logger.Error(response.Errors.First().Description);

            return response.Success;
        }
    }
}
