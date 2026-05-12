using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<Customer> CreateCustomerAsync(CustomerDTO customerDto);
    }
}
