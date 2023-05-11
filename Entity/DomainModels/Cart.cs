﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.DomainModels
{
    public class Cart:IDomainModel
    {

        public Cart()
        {
            CartLines=new List<CartLine>();
        }

        public List<CartLine> CartLines { get; set; }
        public int Total { get; set; }
    }
}
