using DatabaseFirstDWB_LMV.NorthwindData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseFirstDWB_LMV.Backend
{
    public class EmployeesSC : BaseSC
    {
       
        public IQueryable<Employee> GetEmployees()
        {
            return dbContext.Employees.AsQueryable();
        }

        public  Employee GetEmployeeById(int id)
        {
            return GetEmployees().Where(x => x.EmployeeId == id).First();
        }

        public void UpdateEmployeeByID(int id)
        {
            var currentEmployee = new EmployeesSC().GetEmployees().Where(x => x.EmployeeId == id).First();
            currentEmployee.FirstName = "Mauricio";
            dbContext.Employees.Update(currentEmployee);
            dbContext.SaveChanges();
            Console.WriteLine("ID: " + currentEmployee.EmployeeId + " Nombre: " + currentEmployee.FirstName);

            var currentEmployee2 = dbContext.Employees.Where(x => x.EmployeeId == 2).First();
            currentEmployee2.FirstName = "Rolando";
            dbContext.SaveChanges();
        }

        public void DeleteEmployeeById(int id)
        {
            var currentEmployee = GetEmployeeById(id);

            dbContext.Employees.Remove(currentEmployee);
            dbContext.SaveChanges();
        }
    }
}
