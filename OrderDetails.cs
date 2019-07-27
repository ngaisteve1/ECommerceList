using System;
using System.Collections.Generic;
using System.Text;

namespace ecom
{
    class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        public int Customer_Id { get; set; }
        public int ProductId { get; set; }
        public int QuantityOrder { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
