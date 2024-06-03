using CustomersApp.API.Models;

namespace CustomersApp.API.Data
{
    public class CustomerRepository
    {
        private static List<Customer> customers = new List<Customer>
    {
        new Customer { Id = 1, Name = "James Whittington", Email = "james@hotmail.com", Phone = "045432339" },
        new Customer { Id = 2, Name = "Mary Smith", Email = "mary@gmail.com", Phone = "0476323229" }
    };

        public IEnumerable<Customer> GetAll() => customers;

        public Customer GetById(int id) => customers.FirstOrDefault(c => c.Id == id);

        public Customer Add(Customer customer)
        {
            customer.Id = customers.Max(c => c.Id) + 1;
            customers.Add(customer);
            return customer;
        }

        public void Update(Customer customer)
        {
            var existingCustomer = GetById(customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Email = customer.Email;
                existingCustomer.Phone = customer.Phone;
            }
        }

        public void Delete(int id)
        {
            var customer = GetById(id);
            if (customer != null)
            {
                customers.Remove(customer);
            }
        }
    }
}
