
namespace MyApp.Domain.Customers
{
    public class Customer : EntityBase
    {
        public string Email { get; set; }
        public string Name { get; set; }

        public Customer(string email, string name) : base()
        {
            Email = email;
            Name = name;
        }

        public static Customer Add(
            string email,
            string name,
            ICustomerUniquenessChecker customerUniquenessChecker)
        {
            CheckRule(new CustomerEmailMustBeUniqueRule(customerUniquenessChecker, email));

            return new Customer(email, name);
        }
    }
}
