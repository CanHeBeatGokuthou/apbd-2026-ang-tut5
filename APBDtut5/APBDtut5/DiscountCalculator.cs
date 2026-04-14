using System.Text;
using Discount;
using LegacyRenewalApp;
namespace APBDtut5;

public class DiscountCalculator
{
    private readonly IEnumerable<IDiscountRule> rules;
    public DiscountCalculator(IEnumerable<IDiscountRule> rules)
    {
        this.rules = rules;
    }
    public (decimal TotalDiscount, string Notes) CalculateAll(Customer c, SubscriptionPlan p, int seats, decimal baseAmount, bool useLoyaltyPoints)
    {
        decimal totalDiscount = 0;
        var notes = new StringBuilder();
        foreach (var rule in rules)
        {
            var result = rule.Calculate(c, p, seats, baseAmount, useLoyaltyPoints);
            totalDiscount += result.Amount;
            notes.Append(result.Note);
        }
        return (totalDiscount, notes.ToString());
    }
}
