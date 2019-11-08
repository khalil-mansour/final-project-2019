using System;
using System.Collections.Generic;

namespace Web.Api.Core.Domain.Entities
{
    public class HouseQuoteRequest
    {

        public int Id { get; }

        public string UserId { get; }

        public int HouseType { get; }

        public int HouseLocationId { get; }

        public HouseLocation HouseLocation { get; set; }

        public long ListingPrice { get; }

        public DateTime CreatedDate { get; }

        public long DownPayment { get; }

        public long Offer { get; }

        public bool FirstHouse { get; }

        public string Description { get; }

        public List<int> DocumentsId { get; set; }

        public List<File> Documents { get; set; }

        public string MunicipalEvaluationUrl { get; }

        public HouseQuoteRequest(string userId, int houseType, HouseLocation houseLocation, long listingPrice, DateTime createdDate, long downPayment, long offer, bool firstHouse, string description, List<int> documentsId, string municipalEvaluationUrl)
        {
            UserId = userId;
            HouseType = houseType;
            HouseLocation = houseLocation;
            ListingPrice = listingPrice;
            CreatedDate = createdDate;
            DownPayment = downPayment;
            Offer = offer;
            FirstHouse = firstHouse;
            Description = description;
            DocumentsId = documentsId;
            MunicipalEvaluationUrl = municipalEvaluationUrl;
        }

        public HouseQuoteRequest(int id, string userid, int houseType, int houseLocationId, DateTime createdDate, int listingPrice, int downPayment, int offer, bool firstHouse, string description,  string municipalEvaluationUrl)
        {
            Id = id;
            UserId = userid;
            HouseType = houseType;
            HouseLocationId = houseLocationId;
            ListingPrice = listingPrice;
            CreatedDate = createdDate;
            DownPayment = downPayment;
            Offer = offer;
            FirstHouse = firstHouse;
            Description = description;
            MunicipalEvaluationUrl = municipalEvaluationUrl;
        }
    }
}
