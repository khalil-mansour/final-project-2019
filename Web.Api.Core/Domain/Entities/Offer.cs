
namespace Web.Api.Core.Domain.Entities
{
    public class Offer
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

        // update response constructor
        public Offer(int id, string userId, int quoteRequestId, double annualInterestRate, double loan, double mensuality, int rateType, int contractDuration, int loanDuration, int paymentFrequency, string description, bool submitted)
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

        // update request constructor
        public Offer(string userId, double annualInterestRate, double loan, double mensuality, int rateType, int contractDuration, int loanDuration, int paymentFrequency, string description, bool submitted)
        {
            UserId = userId;
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

        // create response constructor
        public Offer(int id, string userId, int quoteRequestId, bool submitted)
        {
            Id = id;
            QuoteRequestId = quoteRequestId;
            UserId = userId;
            Submitted = submitted;
        }

        // create request constructor
        public Offer(string userId, int quoteRequestId, bool submitted)
        {
            UserId = userId;
            QuoteRequestId = quoteRequestId;
            Submitted = submitted;
        }
    }
}
