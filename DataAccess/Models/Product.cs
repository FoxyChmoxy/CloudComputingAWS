﻿using DataAccess.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Product : RdsModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
