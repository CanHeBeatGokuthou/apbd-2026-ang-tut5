namespace Legacy
{
    using LegacyRenewalApp;
    public interface ICustomerRepository { Customer GetById(int customerId); }
    public interface ISubscriptionPlanRepository { SubscriptionPlan GetByCode(string code); }
    public interface IBillingGateway
    {
        void SaveInvoice(RenewalInvoice invoice);
        void SendEmail(string email, string subject, string body);
    }
    public class CustomerRepositoryWrapper : ICustomerRepository
    {
        private readonly CustomerRepository repository = new CustomerRepository();
        public Customer GetById(int customerId) => repository.GetById(customerId);
    }
    public class SubscriptionPlanRepositoryWrapper : ISubscriptionPlanRepository
    {
        private readonly SubscriptionPlanRepository repository = new SubscriptionPlanRepository();
        public SubscriptionPlan GetByCode(string code) => repository.GetByCode(code);
    }
    public class LegacyBillingGatewayWrapper : IBillingGateway
    {
        public void SaveInvoice(RenewalInvoice invoice) => LegacyBillingGateway.SaveInvoice(invoice);
        public void SendEmail(string email, string subject, string body) => LegacyBillingGateway.SendEmail(email, subject, body);
    }
}