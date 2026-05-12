using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerResponseDTO>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            var response = customers.Select(MapToResponseDTO);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponseDTO>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            
            if (customer == null)
            {
                return NotFound();
            }
            
            return Ok(MapToResponseDTO(customer));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerResponseDTO>> RegisterCustomer([FromBody] CustomerDTO customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _customerService.CreateCustomerAsync(customerDto);
            var response = MapToResponseDTO(customer);
            
            return CreatedAtAction(nameof(GetCustomerById), new { id = response.Id }, response);
        }

        private static CustomerResponseDTO MapToResponseDTO(Customer customer)
        {
            return new CustomerResponseDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                EmailId = customer.EmailId,
                MobileNumber = customer.MobileNumber
            };
        }
    }
}
