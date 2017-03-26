/* 
 * This program is a prototype system that allows customers perform a “checkout” based on prices and promotions defined by GroceryCo
 * It takes as input a number of unsorted items in a file and finds a match in another file/price Catalog
 * Developed by Kleanthi Tupe - Mar 25, 2017
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Lib
{
    public class ItemDetails
    {
        public string itemName;
        public int itemCount;

        public decimal itemPrice;
        public decimal discountedPrice;
        public int promoQty;
        public decimal promoPrice;
        public int bogoQty;
        public decimal percentOff;


        public decimal regularTotal;
        public decimal discountedTotal;
        public decimal promoTotal;
        public decimal buyOneGetOneTotal;
        public bool promoApplied;
        public bool discountApplied;
        public bool bogoApplied;

        public ItemDetails(string item, int count)
        {
            itemName = item;
            itemCount = count;

            itemPrice = 0;
            discountedPrice = 0;
            promoQty = 0;
            promoPrice = 0;
            bogoQty = 0;
            percentOff = 0;

            regularTotal = 0;
            discountedTotal = 0;
            promoTotal = 0;
            buyOneGetOneTotal = 0;


            promoApplied = false;
            discountApplied = false;
            bogoApplied = false;
        }


        public void calculateRegCost()
        {
            regularTotal = itemCount * itemPrice;
           
        }

        //Calculates a simple sale discount
        public void calculateDiscountCost()
        {
            if (discountedPrice > 0)
            {
                discountedTotal = itemCount * discountedPrice;
                discountApplied = true;
            }
        }

        //Calculates a promo sale such buy 3 for $1
        public void calculatePromoCost()
        {
            decimal tempQtyHolder = 0;
            if (promoQty > 0 && itemCount >= promoQty && promoPrice >0)
            {
                promoApplied = true;
                tempQtyHolder = itemCount;
                while (tempQtyHolder >= promoQty)
                {
                    promoTotal += promoPrice;
                    tempQtyHolder -= promoQty;
                }
                promoTotal += tempQtyHolder * itemPrice;
                tempQtyHolder = 0;
            }
        }

        //Calculates a BOGO sale, such as buy one get one 50% off
        public void calculateBogoCost()
        {
            decimal tempQtyHolder = 0;
            if (itemCount >= bogoQty + 1 && bogoQty >=1 && percentOff>0 && percentOff <=100)
            {
                
                tempQtyHolder = itemCount;
                while (tempQtyHolder >= bogoQty+1)
                {
                    buyOneGetOneTotal += itemPrice *(bogoQty + (1 - percentOff / 100.0M));
                    tempQtyHolder -= (bogoQty+1);
                }
                buyOneGetOneTotal += tempQtyHolder * itemPrice;
                bogoApplied = true;
            }
        }

    }
}
