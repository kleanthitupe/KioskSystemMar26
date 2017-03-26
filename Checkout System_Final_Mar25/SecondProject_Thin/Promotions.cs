using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Lib
{
    public class Promotions
    {
        

        public Promotions(string item,  decimal dPrice, int pQty, decimal pPrice, int bQty, decimal pOff)
        {
            itemName = item;
            discountedPrice = dPrice;
            promoQty = pQty;
            promoPrice = pPrice;
            bogoQty = bQty;
            percentOff = pOff;
        }

        public string itemName;
        public decimal discountedPrice;
        public int promoQty;
        public decimal promoPrice;
        public int bogoQty;
        public decimal percentOff;
    }
}
