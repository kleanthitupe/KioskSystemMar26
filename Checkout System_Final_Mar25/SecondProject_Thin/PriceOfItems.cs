using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Lib
{
    public class PriceOfItems
    {
        
        public PriceOfItems(string item, decimal price)
        {
            itemName = item;
            itemPrice = price;
        }

        public string itemName;
        public decimal itemPrice;
    }
}
