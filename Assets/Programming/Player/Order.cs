using System;
using System.Collections.Generic;

namespace Assets.Programming.Player {
    public class Order {

        public int NumberOfProducts { get; private set; }
        public List<Product> products;
        /// <summary>
        /// Creates a new order with a random amount of products between 1 and the amount specified
        /// </summary>
        public Order (int maxNumberOfProducts) {
            products = new List<Product>();
            NumberOfProducts = new System.Random().Next(1, maxNumberOfProducts);
            for (int i = 0; i < NumberOfProducts; i++) {
                var p = new Product();
                //This cycle is kind of a way around async methods, it does work, but it could be bad code
                //Change this for async if you can
                do continue;
                while (!p.finishedProduct);
                products.Add(p);
            }
        }
        /// <summary>
        /// Creates a new order with a random amount of products between 1 and the maximum available products
        /// </summary>
        public Order() {
            products = new List<Product>();
            NumberOfProducts = new System.Random().Next(1, Enum.GetNames(typeof(Product.Ingredients)).Length);
            for (int i = 0; i < NumberOfProducts; i++) {
                var p = new Product();
                //This cycle is kind of a way around async methods, it does work, but it could be bad code
                //Change this for async if you can
                do continue;
                while (!p.finishedProduct);
                products.Add(p);
            }
        }

        public override string ToString () {
            string displayInfo = "";
            for (int i = 0; i < products.Count; i++) {
                if (products[i].finishedProduct) {
                    displayInfo += "Gordita #" + (i + 1) + ": \n";
                    //Add values together so it shows Frijoles (2)
                    for (int j = 0; j < products[i].ingredients.Length; j++) {
                        displayInfo += products[i].ingredients[j].ToString() + ".\n";
                    }
                }
            }
            return displayInfo;
        }

    }
}
