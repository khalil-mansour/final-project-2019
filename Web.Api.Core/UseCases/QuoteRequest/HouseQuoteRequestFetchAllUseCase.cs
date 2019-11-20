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
    public sealed class HouseQuoteRequestFetchAllUseCase : IHouseQuoteRequestFetchAllUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IQuoteRequestRepository _quoteRequestRepository;
        private readonly IUserRepository _userRepository;

        public HouseQuoteRequestFetchAllUseCase(IQuoteRequestRepository quoteRequestReposiroty, IUserRepository userRepository)
        {
            _quoteRequestRepository = quoteRequestReposiroty;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(HouseQuoteRequestFetchAllRequest message, IOutputPort<HouseQuoteRequestFetchAllResponse> outputPort)
        {
            // return user to check his type
            var user = await _userRepository.FindById(message.UserId);

            // response object
            HouseQuoteRequestFetchAllRepoResponse response = new HouseQuoteRequestFetchAllRepoResponse(null, false);

            if (user != null)
            {
                // if client, fetch his quote requests
                if (user.User.UserType == 1)
                    response = await _quoteRequestRepository.GetAllQuoteForUser(message.UserId);
                // if broker, fetch all _available_ quote requests
                else
                    response = await _quoteRequestRepository.GetAllQuotes();
            }
            else
            {
                outputPort.Handle(new HouseQuoteRequestFetchAllResponse(new[] { new Error("Action Failed", "No corresponding user with matching ID.") }));
                return true;
            }

            outputPort.Handle(response.Success ? new HouseQuoteRequestFetchAllResponse(response.HouseQuoteRequests, true, null) : new HouseQuoteRequestFetchAllResponse(new[] { new Error("Action Failed", "Unable to fetch house quote requests") }));

            if (!response.Success)
                logger.Error(response.Errors.First().Description);

            return response.Success;
        }
    }
}
