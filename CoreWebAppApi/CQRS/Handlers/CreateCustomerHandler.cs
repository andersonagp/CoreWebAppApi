using CoreWebAppApi.CQRS.Commands;
using CoreWebAppApi.Data;
using CoreWebAppApi.Models;
using MediatR;

namespace CoreWebAppApi.CQRS.Handlers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerRequest, Customer>
    {
        private readonly DbCustomer _context;

        public CreateCustomerHandler(DbCustomer context)
        {
            _context = context;
        }

        public async Task<Customer> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            _context.Customers.Add(request.Customer);

            await _context.SaveChangesAsync();

            return request.Customer;
        }
    }
}
