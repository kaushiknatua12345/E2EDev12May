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
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers(CancellationToken cancellationToken)
        {
            var customers = await _customerService.GetAllCustomersAsync(cancellationToken);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id, CancellationToken cancellationToken)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id, cancellationToken);
            
            if (customer == null)
            {
                return NotFound();
            }
            
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> RegisterCustomer([FromBody] CustomerDTO customerDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _customerService.CreateCustomerAsync(customerDto, cancellationToken);
            
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }
    }
}
