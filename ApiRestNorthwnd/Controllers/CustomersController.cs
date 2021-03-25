using ApiRestNorthwnd.Filters;
using DatabaseFirstDWB_LMV.Backend;
using DatabaseFirstDWB_LMV.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestNorthwnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new CustomerSC().GetCustomers());
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(new CustomerSC().GetCustomerById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CustomerModel newCustomer)
        {
            new CustomerSC().AddCustomer(newCustomer);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] CustomerModel newCustomer)
        {
            new CustomerSC().UpdateCustomer(id, newCustomer);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            new CustomerSC().DeleteCustomerById(id);
            return Ok();
        }
    }
}
