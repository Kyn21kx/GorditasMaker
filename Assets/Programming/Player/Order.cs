﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Programming.Player {
    class Order {

        public int NumberOfProducts {get;}
        public List<Product> products;

        public Order (int maxNumberOfProducts) {
            products = new List<Product>();
            NumberOfProducts = new System.Random().Next(1, maxNumberOfProducts);
            for (int i = 0; i < NumberOfProducts; i++) {
                Task.Run(async () => {
                    products.Add(await CreateProduct());
                });
            }
        }

        public async Task<Product> CreateProduct () {
            return new Product();
        }

    }
}
