using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests.QuoteRequest;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases.QuoteRequest;

namespace Web.Api.Core.UseCases.QuoteRequest
{
    public sealed class HouseQuoteRequestDeleteUseCase : IHouseQuoteRequestDeleteUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IQuoteRequestRepository _quoteRequestRepository;
        private readonly IFileRepository _fileRepository;

        public HouseQuoteRequestDeleteUseCase(IQuoteRequestRepository quoteRequestRepository, IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
            _quoteRequestRepository = quoteRequestRepository;
        }

        public async Task<bool> Handle(HouseQuoteRequestDeleteRequest message, IOutputPort<HouseQuoteRequestDeleteResponse> outputPort)
        {
            var response = await _quoteRequestRepository.Delete(message.HouseQuoteRequestId);

            if (response.Success)
            {
                // instanciate list
                response.HouseQuoteRequest.Documents = new List<Domain.Entities.File>();
                // fetch files
                response.HouseQuoteRequest.DocumentsId.ForEach(x => response.HouseQuoteRequest.Documents.Add(_fileRepository.GetFile(x)));
                // return response
                outputPort.Handle(new HouseQuoteRequestDeleteResponse(response.HouseQuoteRequest, true));
            }
            else
            {
                logger.Error(response.Errors.First()?.Description);
                outputPort.Handle(new HouseQuoteRequestDeleteResponse(new[] { new Error("Delete Failed", "Failed to delete the quote request.") }));
            }

            return response.Success;
        }
    }
}
