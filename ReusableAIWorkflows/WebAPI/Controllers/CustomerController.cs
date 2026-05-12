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
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            _logger.LogInformation("Retrieving all customers.");
            var customers = await _customerService.GetAllCustomersAsync();
            _logger.LogInformation("Successfully retrieved all customers.");
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            _logger.LogInformation("Retrieving customer with ID {CustomerId}.", id);
            var customer = await _customerService.GetCustomerByIdAsync(id);
            
            if (customer == null)
            {
                _logger.LogWarning("Customer with ID {CustomerId} was not found.", id);
                return NotFound();
            }
            
            _logger.LogInformation("Successfully retrieved customer with ID {CustomerId}.", id);
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> RegisterCustomer([FromBody] CustomerDTO customerDto)
        {
            _logger.LogInformation("Registering a new customer.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Customer registration failed due to invalid model state.");
                return BadRequest(ModelState);
            }

            var customer = await _customerService.CreateCustomerAsync(customerDto);
            _logger.LogInformation("Successfully registered customer with ID {CustomerId}.", customer.Id);
            
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }
    }
}
