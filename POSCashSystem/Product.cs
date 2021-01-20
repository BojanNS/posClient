﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSCashSystem
{
    public class Product
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

        public Product(){}
        public Product(string name, string price)
        {
            this.Name = name;
            this.Price = price;
        }
    }
}
