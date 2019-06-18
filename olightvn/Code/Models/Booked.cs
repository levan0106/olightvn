using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olightvn.Models
{
    public class Booked : Base
    {
        public string UserRecId { get; set; }        
        public string OrdererName { get; set; }
        public string OrdererPhone { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverEmail { get; set; }
        public string Note { get; set; }        
        public int ReceiveLocation { get; set; }
        public int Quantity { get; set; }
        public int ProcessStatus { get; set; }
        public string ProcessName { get; set; }
        public int LocationId { get; set; }
        public string Thumbnail { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Deposit { get; set; }
    }
   
}
