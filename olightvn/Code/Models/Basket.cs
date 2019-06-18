using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olightvn.Models
{
    public class Basket:Base
    {
        public string UserRecId { get; set; }        
        public int CurrentStep { get; set; }
        public string OrdererName { get; set; }
        public string OrdererPhone { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverPhone { get; set; }
        public int ReceiveLocation { get; set; }
        public string Note { get; set; }
        public IEnumerable<ProductInBasket> Products { get; set; }
    }
    public class ProductInBasket:Product
    {
        //public int Quantity { get; set; } 

    }
}
