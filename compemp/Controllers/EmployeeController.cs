using compemp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace compemp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly CompEmp _compemp;
        public EmployeeController(CompEmp empComp)
        {
            _compemp = empComp;
        }
        [HttpGet]
        public async Task<ActionResult> Get(object emps)
        {

            var emp = await _compemp.Employees.Include(c => c.Company).ToListAsync();
            return Ok(emp);
        }
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Employee>> GetById(int id)
        {
            var emp = await _compemp.Employees.Include(c => c.Company).Where(e => e.Id == id).FirstOrDefaultAsync();

            return Ok(emp);

        }
        [HttpPost("AddEmployee")]
        public async Task<ActionResult<Employee>> AddEmployee(Employee emp)
        {
            await _compemp.Employees.AddAsync(emp);
            _compemp.SaveChanges();
            return Ok("");
        }
        [HttpPut("update")]
        public async Task<ActionResult<Employee>> update(Employee employee)
        {

            var emp = await _compemp.Employees.Include(c => c.Company).FirstOrDefaultAsync();
            emp.Name = employee.Name;
            emp.Age = employee.Age;
            _compemp.SaveChanges();

            return Ok();
        }
        [HttpDelete("DeleteEmployee")]
        public async Task<ActionResult<List<Employee>>> Delete(int id)
        {
            var emp = await _compemp.Employees.Include(c => c.Company).FirstOrDefaultAsync();
            _compemp.Remove(emp);
            return Ok();
        }

    }
}
