using Web.Api.Core.Dto.UseCaseResponses.Offer;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests.Offer
{
    public class OfferUpdateRequest : IUseCaseRequest<OfferUpdateResponse>
    {
        public int Id { get; }

        public string UserId { get; }

        public int QuoteRequestId { get; }

        public double AnnualInterestRate { get; }

        public double Loan { get; }

        public double Mensuality { get; }

        public int RateType { get; }

        public int ContractDuration { get; }

        public int LoanDuration { get; }

        public int PaymentFrequency { get; }

        public string Description { get; }

        public bool Submitted { get; }

        public OfferUpdateRequest(string userId, int quoteRequestId, double annualInterestRate, double loan, double mensuality, int rateType, int contractDuration, int loanDuration, int paymentFrequency, string description, bool submitted)
        {
            UserId = userId;
            QuoteRequestId = quoteRequestId;
            AnnualInterestRate = annualInterestRate;
            Loan = loan;
            Mensuality = mensuality;
            RateType = rateType;
            ContractDuration = contractDuration;
            LoanDuration = loanDuration;
            PaymentFrequency = paymentFrequency;
            Description = description;
            Submitted = submitted;
        }

        public OfferUpdateRequest(int id, string userId, int quoteRequestId, double annualInterestRate, double loan, double mensuality, int rateType, int contractDuration, int loanDuration, int paymentFrequency, string description, bool submitted)
        {
            Id = id;
            UserId = userId;
            QuoteRequestId = quoteRequestId;
            AnnualInterestRate = annualInterestRate;
            Loan = loan;
            Mensuality = mensuality;
            RateType = rateType;
            ContractDuration = contractDuration;
            LoanDuration = loanDuration;
            PaymentFrequency = paymentFrequency;
            Description = description;
            Submitted = submitted;
        }
    }
}
