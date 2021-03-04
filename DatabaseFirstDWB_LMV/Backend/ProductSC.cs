using DatabaseFirstDWB_LMV.NorthwindData;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseFirstDWB_LMV.Backend
{
    public class ProductSC : BaseSC
    {
        public void AddNewProduct(string productName, decimal price)
        {
            var newProduct = new NorthwindData.Product();
            newProduct.ProductName = productName;
            newProduct.UnitPrice = price;

            //dbContext.Add(newProduct);
            dbContext.Products.Add(newProduct);
            dbContext.SaveChanges();
        }
    }
}
