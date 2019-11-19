using System.Collections.Generic;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests.QuoteRequest
{
    public class HouseQuoteRequestUpdateRequest : IUseCaseRequest<HouseQuoteRequestUpdateResponse>
    {
        public int QuoteRequestId { get; }
        public string UserId { get; }
        public int HouseType { get; }
        public HouseLocationRequest HouseLocationRequest { get; }
        public long ListingPrice { get; }
        public long DownPayment { get; }
        public long Offer { get; }
        public bool FirstHouse { get; }
        public string Description { get; }
        public List<int> DocumentsId { get; }
        public string MunicipalEvaluationUrl { get; }

        public HouseQuoteRequestUpdateRequest(int quoteRequestId, string userId, int houseType,
            HouseLocationRequest houseLocationRequest, long listingPrice, long downPayment,
            long offer, bool firstHouse, string description, List<int> documentsId, string municipalEvaluationUrl)
        {
            QuoteRequestId = quoteRequestId;
            UserId = userId;
            HouseType = houseType;
            HouseLocationRequest = houseLocationRequest;
            ListingPrice = listingPrice;
            DownPayment = downPayment;
            Offer = offer;
            FirstHouse = firstHouse;
            Description = description;
            DocumentsId = documentsId;
            MunicipalEvaluationUrl = municipalEvaluationUrl;
        }
    }
}
