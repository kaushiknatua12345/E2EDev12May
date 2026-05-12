using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> CreateCustomerAsync(CustomerDTO customerDto)
        {
            var customer = new Customer
            {
                Name = customerDto.Name,
                EmailId = customerDto.EmailId,
                MobileNumber = customerDto.MobileNumber
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }
    }
}
