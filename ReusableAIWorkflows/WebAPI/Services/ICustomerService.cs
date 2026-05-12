using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken = default);
        Task<Customer?> GetCustomerByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Customer> CreateCustomerAsync(CustomerDTO customerDto, CancellationToken cancellationToken = default);
    }
}
