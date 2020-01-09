﻿namespace Domain.Customers
{
    using System.Threading.Tasks;
    using Domain.Customers.ValueObjects;

    /// <summary>
    /// Customer <see href="https://github.com/ivanpaulovich/clean-architecture-manga/wiki/Domain-Driven-Design-Patterns#domain-service">Domain Service Domain-Driven Design Pattern</see>.
    /// </summary>
    public class CustomerService
    {
        private readonly ICustomerFactory _customerFactory;
        private readonly ICustomerRepository _customerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerService"/> class.
        /// </summary>
        /// <param name="customerFactory">Customer Factory.</param>
        /// <param name="customerRepository">Customer Repository.</param>
        public CustomerService(
            ICustomerFactory customerFactory,
            ICustomerRepository customerRepository)
        {
            this._customerFactory = customerFactory;
            this._customerRepository = customerRepository;
        }

        /// <summary>
        /// Creates a Customer.
        /// </summary>
        /// <param name="ssn">SSN.</param>
        /// <param name="name">Name.</param>
        /// <returns>Created Customer.</returns>
        public async Task<ICustomer> CreateCustomer(SSN ssn, Name name)
        {
            var customer = this._customerFactory.NewCustomer(ssn, name);
            await this._customerRepository.Add(customer);
            return customer;
        }

        /// <summary>
        /// Verifies if the customer is already registered.
        /// </summary>
        /// <param name="customerId">Customer Id.</param>
        /// <returns>True if he is registered.</returns>
        public async Task<bool> IsCustomerRegistered(CustomerId customerId)
        {
            try
            {
                var customer = await this._customerRepository.GetBy(customerId);
                return true;
            }
            catch (CustomerNotFoundException)
            {
                return false;
            }
        }
    }
}
