using DatabaseFirstDWB_LMV.DataAccess;
using System;
using System.Linq;
using DatabaseFirstDWB_LMV.NorthwindData;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirstDWB_LMV
{
    class Program
    {
        public static NORTHWNDContext dbContext = new NORTHWNDContext();
        public static void InitialTest()
        {

            DWBLMVContext dbContext = new DWBLMVContext();

            //SELECT *FROM PRODUCTS
            var productsQry = dbContext.Products;
            var productList = productsQry.ToList();

            //select Title as Titulo, Cost as Costo from Products : //Este es un objeto anonimo
            var productSelectQry = dbContext.Products.Select(s => new
            {   //Proyección lambda
                Titulo = s.Title,
                Costo = s.Cost
            }).ToList();

            productSelectQry.ForEach(f => Console.WriteLine(f.Titulo));


        }

        public static void Excercise1()
        {
            //SELECT *FROM Employees; 
            var employeeQry = new NORTHWNDContext().Employees.AsQueryable();
            var output = employeeQry.ToList();
        }

        public static void Excercise2()
        {
            // Objeto NO anonimo
            //var x = new ObjectClass{ Id = 2, Title = "jkhsfjs" };
            //Objeto ANONIMO
            //var x = new { Id = 2, Title = "jkhsfjs" };

            //SELECT Title, FirstName, LastName FROM Employees WHERE Title = 'Sales Representative';
            var employeeQry = new NORTHWNDContext()
                .Employees.Select(s => new 
                {
                     s.Title,
                     s.FirstName,
                     s.LastName
                }).Where(w=> w.Title == "Sales Representative").AsQueryable();

            //Aqui materializamos el query
            var output = employeeQry.ToList();

            //var od = new NORTHWNDContext().Products.Include(i => i.OrderDetails)
            //    .Where(W => W.ProductId == 1)
            //    .SelectMany(s => s.OrderDetails)
            //    .Select(s => s.Order);
            //var o = od.ToList();

            output.ForEach(fe => Console.WriteLine("Name: " + fe.FirstName + " Title: " + fe.Title));

        }

        public static void Excercise3()
        {

            //SELECT FirstName as Nombre, LastName as Apellido  FROM Employees WHERE Title<> 'Sales Representative
            var employeeQry = new NORTHWNDContext()
                .Employees.Select(s => new
                {
                    Nombre = s.FirstName,
                    Apellido = s.LastName,
                    s.Title,
                }).Where(w => w.Title != "Sales Representative");

            var output = employeeQry.ToList();

        }

        public static void Excercise4()
        {
            // UPDATE Employees SET NAME = ‘Nadia’ WHERE ID = 1; 
            
            var currentEmployee = dbContext.Employees.Where(x => x.EmployeeId == 1).First();
            currentEmployee.FirstName = "Mauricio";
            dbContext.Employees.Update(currentEmployee);
            dbContext.SaveChanges();
            Console.WriteLine("ID: " + currentEmployee.EmployeeId + " Nombre: " + currentEmployee.FirstName);

            var currentEmployee2 = dbContext.Employees.Where(x => x.EmployeeId == 2).First();
            currentEmployee2.FirstName = "Rolando";
            dbContext.SaveChanges();

        }

        public static void Excercise5()
        {
            //insert into Products (ProductName, UnitPrice) values ('coca cola', 12.50)
            var newProduct = new NorthwindData.Product();
            newProduct.ProductName = "Coca cola";
            newProduct.UnitPrice = 12.50m;

            //dbContext.Add(newProduct);
            dbContext.Products.Add(newProduct);
            dbContext.SaveChanges();
            
            

        }

        public static void Excercise6(int id)
        {
            var currentEmployee = dbContext.Employees.Where(x => x.EmployeeId == id).First();

            dbContext.Employees.Remove(currentEmployee);
            dbContext.SaveChanges();

        }

        //  Obtener los productos, el cliente y el empleado por Id de Order
        public static void Excercise7(int orderID)
        {
            var qry = dbContext.Orders.Where(w => w.OrderId == orderID).Select(s => new
            {

                Empleado = s.Employee.FirstName,
                Cliente = s.Customer.ContactName,
                Products = s.OrderDetails.Select(sel => sel.Product.ProductName)
            });

            var result = qry.ToList();
        }
        static void Main(string[] args)
        {
            //Excercise1();
            //Excercise2();
            //Excercise3();
            //Excercise4();
            //Excercise5();
            //Excercise6(9);
            Excercise7(10248);

        }
    }
}
