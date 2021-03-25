using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseFirstDWB_LMV.Backend;
using DatabaseFirstDWB_LMV.NorthwindData;
using DatabaseFirstDWB_LMV.Models;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestNorthwnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class EmployeeController : ControllerBase
    {
        // GET: api/<EmployeeController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //     OK(json)
                return Ok(new EmployeesSC().GetEmployees().ToList());
            }
            catch (Exception ex)
            {
                return ThrowInternalErrorException(ex);
            }
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(new EmployeesSC().GetEmployeeById(id));
            }
            catch (Exception ex)
            {
                return ThrowInternalErrorException(ex);
            }
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public IActionResult Post([FromBody] EmployeeModel newEmployee)
        {
            try
            {
                new EmployeesSC().AddEmployee(newEmployee);
                return Ok();
            }
            catch (Exception ex)
            {
                return ThrowInternalErrorException(ex);
            }

        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                new EmployeesSC().DeleteEmployeeById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return ThrowInternalErrorException(ex);
            }
        }

        #region helpers
        private IActionResult ThrowInternalErrorException(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

        #endregion
    }
}
