using DatabaseFirstDWB_LMV.Models;
using DatabaseFirstDWB_LMV.NorthwindData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseFirstDWB_LMV.Backend
{
    public class CustomerSC : BaseSC
    {

        public IQueryable<Customer> GetCustomers()
        {
            return dbContext.Customers.AsQueryable();
        }

        public Customer GetCustomerById(string id)
        {
            var customer = GetCustomers().FirstOrDefault(f => f.CustomerId == id);
            if (customer == null)
                throw new Exception("No existe el cliente con el id proporcionado");

            return customer;
        }

        public void AddCustomer(CustomerModel newCustomer)
        {

            // Formato muy parecido a JSON, notación de objetos
            var newCustomerRegister = new Customer()
            {
                CompanyName = newCustomer.Comany,
                ContactName = newCustomer.CustomerName,
                Phone = newCustomer.Phone,
                CustomerId = newCustomer.Comany.Substring(0, 1) + newCustomer.Phone.Substring(0, 3)
            };

            dbContext.Customers.Add(newCustomerRegister);
            dbContext.SaveChanges();


        }

        public void UpdateCustomer(string id, CustomerModel customerForUpdate)
        {
            var currentCustomer = GetCustomerById(id);

            currentCustomer.ContactName = customerForUpdate.CustomerName;
            currentCustomer.Phone = customerForUpdate.Phone;
            currentCustomer.CompanyName = customerForUpdate.Comany;

            dbContext.SaveChanges();
        }

        public void DeleteCustomerById(string id)
        {

            var currentCustomer = GetCustomerById(id);
            dbContext.Customers.Remove(currentCustomer);
            dbContext.SaveChanges();
        }
    }
}
