using CoreWebAppApi.Models;
using MediatR;

namespace CoreWebAppApi.CQRS.Commands
{ 
    public class CreateCustomerRequest : IRequest<Customer>
    {
        public Customer Customer { get; set; }
    }
}