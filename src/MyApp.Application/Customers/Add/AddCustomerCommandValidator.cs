using Dapper;
using FluentValidation;
using FluentValidation.Validators;
using MyApp.Application.Configuration.Data;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Customers.Add
{
    public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public AddCustomerCommandValidator(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is invalid");

            //Customer Email must be unique
            RuleFor(x => x.Email)
                .CustomAsync(CustomerEmailMustBeUniqueAsync);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must be less than 100 characters");
        }

        private async Task CustomerEmailMustBeUniqueAsync(string email, CustomContext customContext, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT TOP 1 1 " +
                               "FROM [dbo].[Customers] AS [Customer] " +
                               "WHERE [Customer].[Email] = @Email";
            var customersNumber = await connection.QuerySingleOrDefaultAsync<int?>(sql,
                            new
                            {
                                Email = email
                            });
            if (customersNumber.HasValue)
            {
                customContext.AddFailure($"Customer with this email({email}) already exists");
            }
        }
    }
}
