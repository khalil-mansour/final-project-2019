using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Domain.Entities
{
    public class HouseQuoteRequest
    {

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("house_type_id")]
        public int HouseType { get; }

        [JsonProperty("house_location")]
        public HouseLocation HouseLocation {get;}

        [JsonProperty("listing")]
        public long ListingPrice { get;  }

        [JsonProperty("down_payment")]
        public long DownPayment { get; }

        [JsonProperty("offer")]
        public long Offer { get;}

        [JsonProperty("first_house")]
        public bool FirstHouse { get; }

        [JsonProperty("description")]
        public string Description { get; }

        [JsonProperty("municipal_evaluation")]
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

    }
}
