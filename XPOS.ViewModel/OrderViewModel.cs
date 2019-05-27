using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPOS.ViewModel
{
    public class OrderHeaderViewModel
    {
        public long Id { get; set; }

        public string Reference { get; set; }

        public decimal Amount { get; set; }

        public bool Active { get; set; }

        public List<OrderDetailViewModel> Details { get; set; }
    }

    public class OrderDetailViewModel
    {
        public OrderDetailViewModel()
        {
            Quantity = 1;
        }

        public long Id { get; set; }

        public long HeaderId { get; set; }

        public long ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Amount {
            get { return (Quantity * Price); }
            set {}
        }

        public bool Active { get; set; }
    }
}
