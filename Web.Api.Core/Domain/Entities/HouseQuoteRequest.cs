using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Domain.Entities
{
    public class HouseQuoteRequest
    {

        public int Id { get; }

        public int HouseType { get; }

        public HouseLocation HouseLocation {get;}

        public long ListingPrice { get;  }

        public long DownPayment { get; }

        public long Offer { get;}

        public bool FirstHouse { get; }

        public string Description { get; }

        public string MunicipalEvaluationUrl { get; }

        internal HouseQuoteRequest(int id, int houseType, HouseLocation houseLocation, long listing, long downPayement, long offer, bool firstHouse, string description, string municipalEvaluation)
        {
            Id = id;
            HouseType = houseType;
            HouseLocation = houseLocation;
            ListingPrice = listing;
            DownPayment = downPayement;
            Offer = offer; 
            FirstHouse = firstHouse;
            Description = description;
            MunicipalEvaluationUrl = municipalEvaluation;
        }

        public HouseQuoteRequest(int houseType, HouseLocation houseLocation, long listingPrice, long downPayment, long offer, bool firstHouse, string description, string municipalEvaluationUrl)
        {
            HouseType = houseType;
            HouseLocation = houseLocation;
            ListingPrice = listingPrice;
            DownPayment = downPayment;
            Offer = offer;
            FirstHouse = firstHouse;
            Description = description;
            MunicipalEvaluationUrl = municipalEvaluationUrl;
        }
    }
}
