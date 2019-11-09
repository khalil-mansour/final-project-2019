namespace Web.Api.Core.Domain.Entities
{
    public class FinancialCapacity
    {
        public string Id { get; }

        public int AnnualIncome { get; }

        public int DownPayment { get; }

        public int MensualDebt { get; }

        public float InterestRate { get; }

        public int MunicipalTaxes { get; }

        public int HeatingCost { get; }

        public int CondoFee { get; }

        public static FinancialCapacity Default(string id)
        { return new FinancialCapacity(id, 0, 0, 0, 4.4f, 0, 0, 0); }

        internal FinancialCapacity(string id,
         int annualIncome, 
         int downPayment, 
         int mensualDebt, 
         float interestRate, 
         int municipalTaxes, 
         int heatingCost, 
         int condoFee)
        {
            Id = id;
            AnnualIncome = annualIncome;
            DownPayment = downPayment;
            MensualDebt = mensualDebt;
            InterestRate = interestRate;
            MunicipalTaxes = municipalTaxes;
            HeatingCost = heatingCost;
            CondoFee = condoFee;            
        }
    }
}
