using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests.QuoteRequest;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases.QuoteRequest;

namespace Web.Api.Core.UseCases.QuoteRequest
{
    public sealed class HouseQuoteRequestGetDetailUseCase : IHouseQuoteRequestGetDetailRequestUseCase
    {

        private readonly IQuoteRequestRepository _quoteRequestRepository;
        private readonly IFileRepository _fileRepository;

        public HouseQuoteRequestGetDetailUseCase(IQuoteRequestRepository quoteRequestReposiroty, IFileRepository fileRepository) {
            _quoteRequestRepository = quoteRequestReposiroty;
            _fileRepository = fileRepository;
        }

        public async Task<bool> Handle(HouseQuoteRequestGetDetailRequest message, IOutputPort<HouseQuoteRequestGetDetailResponse> outputPort)
        {
            List<File> files = new List<File>();
            var quoteDetailResponse = await _quoteRequestRepository.GetDetailFor(message.QuoteRequestId);

            quoteDetailResponse.HouseQuoteRequest.DocumentsId.ForEach(x => files.Add(_fileRepository.GetFile(x)));
            quoteDetailResponse.HouseQuoteRequest.Documents = files;

            outputPort.Handle(quoteDetailResponse.Success ? new HouseQuoteRequestGetDetailResponse(quoteDetailResponse.HouseQuoteRequest, true, null) : new HouseQuoteRequestGetDetailResponse(new[] { new Error("Action Failed", "Enable to get detail for house quote request") }));
            return quoteDetailResponse.Success;
        }
    }

}
