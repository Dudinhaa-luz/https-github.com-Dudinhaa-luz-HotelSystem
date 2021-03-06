﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    class ProductIncome
    {
        public int ID { get; set; }
        public DateTime EntryDate { get; set; }
        public int EmployeesID { get; set; }
        public double TotalValue { get; set; }
        public int SuppliersID { get; set; }
        public List<ProductsIncomeDetail> Items { get; set; }
    }
}
