using System;
using LegacyRenewalApp;
namespace Discount
{
    public interface IDiscountRule
    {
        (decimal Amount, string Note) Calculate(Customer customer, SubscriptionPlan plan, int seatCount, decimal baseAmount, bool useLoyaltyPoints);
    }
    public class SegmentDiscountRule : IDiscountRule
    {
        public (decimal Amount, string Note) Calculate(Customer c, SubscriptionPlan p, int seats, decimal baseAmount, bool useLoyaltyPoints) => c.Segment switch
        {
            "Platinum" => (baseAmount * 0.15m, "platinum discount; "),
            "Gold" => (baseAmount * 0.10m, "gold discount; "),
            "Silver" => (baseAmount * 0.05m, "silver discount; "),
            "Education" when p.IsEducationEligible => (baseAmount * 0.20m, "education discount; "),
            _ => (0m, string.Empty)
        };
    }
    public class LoyaltyDiscountRule : IDiscountRule
    {
        public (decimal Amount, string Note) Calculate(Customer c, SubscriptionPlan p, int seats, decimal baseAmount, bool useLoyaltyPoints) =>
            c.YearsWithCompany >= 5 ? (baseAmount * 0.07m, "long-term loyalty discount; ") :
            c.YearsWithCompany >= 2 ? (baseAmount * 0.03m, "basic loyalty discount; ") : (0m, string.Empty);
    }
    public class VolumeDiscountRule : IDiscountRule
    {
        public (decimal Amount, string Note) Calculate(Customer c, SubscriptionPlan p, int seats, decimal baseAmount, bool useLoyaltyPoints) =>
            seats >= 50 ? (baseAmount * 0.12m, "large team discount; ") :
            seats >= 20 ? (baseAmount * 0.08m, "medium team discount; ") :
            seats >= 10 ? (baseAmount * 0.04m, "small team discount; ") : (0m, string.Empty);
    }
    public class PointsDiscountRule : IDiscountRule
    {
        public (decimal Amount, string Note) Calculate(Customer c, SubscriptionPlan p, int seats, decimal baseAmount, bool useLoyaltyPoints)
        {
            if (!useLoyaltyPoints || c.LoyaltyPoints <= 0) return (0m, string.Empty);
            int points = Math.Min(c.LoyaltyPoints, 200);
            return (points, $"loyalty points used: {points}; ");
        }
    }
}