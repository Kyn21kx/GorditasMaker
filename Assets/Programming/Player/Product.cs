using System;
using System.Collections.Generic;

namespace Assets.Programming.Player {
    public class Product {
        public enum Ingredients { 
            Frijoles, Chicharron, Queso,
            Papas, Crema, Guacamole, Salsa
        };
        public Ingredients[] ingredients;
        private List<int> repeatedValues;
        public bool finishedProduct;
        public Product () {
            finishedProduct = false;
            RandomGeneration();
        }
        private void RandomGeneration() {
            repeatedValues = new List<int>();
            var values = Enum.GetValues(typeof(Ingredients));
            int numberOfIngredients = new System.Random().Next(1, Enum.GetNames(typeof(Ingredients)).Length);
            ingredients = new Ingredients[numberOfIngredients];
            for (int i = 0; i < numberOfIngredients; i++) {
                int init = new System.Random().Next(ingredients.Length);
                var newIng = (Ingredients)values.GetValue(init);
                if (!repeatedValues.Contains(init)) {
                    repeatedValues.Add(init);
                    ingredients.SetValue(newIng, i);
                }
                else {
                    i--;
                }
            }
            finishedProduct = true;
        }
    }
}
