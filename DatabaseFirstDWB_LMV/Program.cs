using DatabaseFirstDWB_LMV.DataAccess;
using System;
using System.Linq;
using DatabaseFirstDWB_LMV.NorthwindData;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstDWB_LMV.Backend;

namespace DatabaseFirstDWB_LMV
{
    class Program
    {
        
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
            var employeeQry = new EmployeesSC().GetEmployees();
            var output = employeeQry.ToList();
        }

        public static void Excercise2()
        {
            // Objeto NO anonimo
            //var x = new ObjectClass{ Id = 2, Title = "jkhsfjs" };
            //Objeto ANONIMO
            //var x = new { Id = 2, Title = "jkhsfjs" };

            //SELECT Title, FirstName, LastName FROM Employees WHERE Title = 'Sales Representative';
            var employeeQry = new EmployeesSC().GetEmployees().Select(s => new 
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
            var employeeQry = new EmployeesSC().GetEmployees().Select(s => new
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
            new EmployeesSC().UpdateEmployeeByID(1);
        }

        public static void Excercise5()
        {
            //insert into Products (ProductName, UnitPrice) values ('coca cola', 12.50)
            new ProductSC().AddNewProduct("Coca cola", 12.50m);
        }

        public static void Excercise6(int id)
        {
            new EmployeesSC().DeleteEmployeeById(id);
        }

        //  Obtener los productos, el cliente y el empleado por Id de Order
        public static void Excercise7(int orderID)
        {
            var qry = new OrderSC().GetOrderById(orderID).Select(s => new
            {

                Empleado = s.Employee.FirstName,
                Cliente = s.Customer.ContactName,
                Products = s.OrderDetails.Select(sel => sel.Product.ProductName)
            });

            var result = qry.ToList();
        }

       

        static void Main(string[] args)
        {
            Excercise1();
            Excercise2();
            Excercise3();
            Excercise4();
            Excercise5();
            Excercise6(9);
            Excercise7(10248);

        }
    }
}
