using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses.QuoteRequest
{
    public class HouseQuoteRequestGetDetailResponse : UseCaseResponseMessage
    {
        public HouseQuoteRequest HouseQuoteRequest { get; }

        public IEnumerable<Error> Errors { get; }

        public HouseQuoteRequestGetDetailResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public HouseQuoteRequestGetDetailResponse(HouseQuoteRequest houseQuoteRequest, bool success = false, string message = null) : base(success, message)
        {
            HouseQuoteRequest = houseQuoteRequest;
        }
    }
}
