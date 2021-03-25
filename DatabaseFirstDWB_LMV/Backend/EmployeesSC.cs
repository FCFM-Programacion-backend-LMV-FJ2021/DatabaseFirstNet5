using DatabaseFirstDWB_LMV.Models;
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
            var employee =  GetEmployees().Where(x => x.EmployeeId == id).FirstOrDefault();

            if (employee == null)
                throw new Exception("El usuario con el id solicitado, no existe");

            return employee;
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

            try
            {
                dbContext.Employees.Remove(currentEmployee);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo guardar el cambio en base de datos: "
                    + ex.Message + ". " + ex.InnerException != null ?  ex.InnerException.Message : "");
            }
        }

        public void AddEmployee(EmployeeModel newEmployee)
        {

            var newEmployeeReg = new Employee();
            newEmployeeReg.FirstName = newEmployee.Name;
            newEmployeeReg.LastName = newEmployee.LastName;
            
            dbContext.Employees.Add(newEmployeeReg);
            dbContext.SaveChanges();

        }
    }
}
