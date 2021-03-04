using DatabaseFirstDWB_LMV.NorthwindData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseFirstDWB_LMV.Backend
{
    public class OrderSC : BaseSC
    {
      
        public IQueryable<Order> GetOrderById(int orderID)
        {
            return GetAllOrders().Where(w => w.OrderId == orderID);
        }

        private IQueryable<Order> GetAllOrders()
        {
            return dbContext.Orders;
        }

   
    }
}
